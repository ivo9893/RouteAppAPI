using Microsoft.EntityFrameworkCore;
using RouteAppAPI.Data;
using RouteAppAPI.Models;
using RouteAppAPI.Models.DTO;
using RouteAppAPI.Services.Interfaces;

namespace RouteAppAPI.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<object>> RegisterUser(UserRegistrationDto user)
        {
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == user.Email || u.Username == user.Username);

            if (existingUser != null)
            {
                var errors = new List<string>();

                if (existingUser.Email == user.Email)
                    errors.Add("Email");

                if (existingUser.Username == user.Username)
                    errors.Add("Username");

                return new ApiResponse<object>
                {
                    Success = false,
                    Message = "Registration failed.",
                    Errors = errors
                };
            }

            var newUser = new User
            {
                Username = user.Username,
                Email = user.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password),
                FirstName = user.FirstName,
                LastName = user.LastName,
                Location = user.Location,
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return new ApiResponse<object>
            {
                Success = true,
                Message = "User registered successfully."
            };
        }


    }
}
