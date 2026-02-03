
using RouteAppAPI.Data;
using RouteAppAPI.Models;
using RouteAppAPI.Models.DTO;
using RouteAppAPI.Services.Interfaces;
using System.Security.Claims;
using System.Text;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;

namespace RouteAppAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<AuthResponse> LoginAsync(UserLoginDto loginDto)
        {

            if (loginDto.Password == null)
            {
                //_logger.LogWarning("Login attempt with null password for email: {Email}", loginDto.Email);
                return null;
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
            {
                //_logger.LogWarning("Failed login attempt for email: {Email}", loginDto.Email);
                return null; // Invalid credentials
            }

            //_logger.LogInformation("User {Email} logged in successfully", loginDto.Email);

            // Generate Access Token
            var accessToken = GenerateJwtToken(user);
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var accessTokenExpiry = DateTime.UtcNow.AddMinutes(
                jwtSettings.GetValue<int>("TokenExpirationMinutes"));

            // Generate and Save Refresh Token
            var refreshToken = GenerateRefreshToken();
            var refreshTokenHash = HashToken(refreshToken);
            var refreshTokenExpiry = DateTime.UtcNow.AddDays(
                jwtSettings.GetValue<int>("RefreshTokenExpirationDays"));

            var newRefreshToken = new Token
            {
                RefreshToken = refreshTokenHash,
                ExpiryDate = refreshTokenExpiry,
                UserId = user.Id
            };

            _context.Tokens.Add(newRefreshToken);
            await _context.SaveChangesAsync();

            return new AuthResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                AccessTokenExpiry = accessTokenExpiry,
                RefreshTokenExpiry = refreshTokenExpiry,
                UserId = user.Id,
                UserFirstName = user.FirstName ?? "",
                UserLastName = user.LastName ?? "",
                UserLocation = user.Location ?? ""
            };


        }

        public async Task<AuthResponse> RefreshTokenAsync(string refreshToken)
        {
            var refreshTokenHash = HashToken(refreshToken);
            var existingRefreshToken = await _context.Tokens
                                                    .Include(rt => rt.User) // Eager load user
                                                    .SingleOrDefaultAsync(rt => rt.RefreshToken == refreshTokenHash);

            if (existingRefreshToken == null || existingRefreshToken.IsRevoked || existingRefreshToken.ExpiryDate <= DateTime.UtcNow)
            {
               //_logger.LogWarning("Invalid, revoked, or expired refresh token attempt");
                return null; // Invalid, revoked, or expired refresh token
            }

            //_logger.LogInformation("Refreshing token for user {UserId}", existingRefreshToken.User.Id);

            // Revoke the old refresh token (one-time use pattern)
            existingRefreshToken.RevokedAt = DateTime.UtcNow;
            _context.Tokens.Update(existingRefreshToken);

            // Generate new Access Token
            var newAccessToken = GenerateJwtToken(existingRefreshToken.User);
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var newAccessTokenExpiry = DateTime.UtcNow.AddMinutes(
                jwtSettings.GetValue<int>("TokenExpirationMinutes"));

            // Generate new Refresh Token
            var newRefreshTokenValue = GenerateRefreshToken();
            var newRefreshTokenHash = HashToken(newRefreshTokenValue);
            var newRefreshTokenExpiry = DateTime.UtcNow.AddDays(
                jwtSettings.GetValue<int>("RefreshTokenExpirationDays"));

            var newRefreshToken = new Token
            {
                RefreshToken = newRefreshTokenHash,
                ExpiryDate = newRefreshTokenExpiry,
                UserId = existingRefreshToken.User.Id
            };

            _context.Tokens.Add(newRefreshToken);
            await _context.SaveChangesAsync();

            return new AuthResponse
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshTokenValue,
                AccessTokenExpiry = newAccessTokenExpiry,
                RefreshTokenExpiry = newRefreshTokenExpiry
            };
        }

        private string GenerateJwtToken(User user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secret = jwtSettings.GetValue<string>("Secret");
            var issuer = jwtSettings.GetValue<string>("Issuer");
            var audience = jwtSettings.GetValue<string>("Audience");
            var expirationMinutes = jwtSettings.GetValue<int>("TokenExpirationMinutes");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}")
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expirationMinutes),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        private static string HashToken(string token)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(token));
            return Convert.ToBase64String(bytes);
        }
    }
}
