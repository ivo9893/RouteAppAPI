using RouteAppAPI.Models.DTO;

namespace RouteAppAPI.Services.Interfaces
{
    public interface IUserService
    {
        Task<ApiResponse<object>> RegisterUser(UserRegistrationDto user);
    }
}
