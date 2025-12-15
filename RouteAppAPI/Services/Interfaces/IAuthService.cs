using RouteAppAPI.Models.DTO;

namespace RouteAppAPI.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> LoginAsync(UserLoginDto user);
        Task<AuthResponse> RefreshTokenAsync(string refreshToken);
    }
}
