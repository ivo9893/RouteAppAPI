using RouteAppAPI.Models.DTO;

namespace RouteAppAPI.Services.Interfaces
{
    public interface IRouteTypeService
    {
        Task<List<RouteTypeDto>> GetRouteTypesAsync();
        Task<RouteTypeDto?> GetRouteTypeAsync(int id);
    }
}
