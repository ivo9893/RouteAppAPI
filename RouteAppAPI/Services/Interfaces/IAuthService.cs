using RouteAppAPI.Models.DTO;

namespace RouteAppAPI.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> LoginAsync(UserLoginDto user);
        Task<AuthResponseDto> RefreshTokenAsync(string refreshToken);
    }
}
