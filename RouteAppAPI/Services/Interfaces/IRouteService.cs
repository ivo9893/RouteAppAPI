using CloudinaryDotNet.Actions;
using RouteAppAPI.Models;
using RouteAppAPI.Models.DTO;

namespace RouteAppAPI.Services.Interfaces
{
    public interface IRouteService
    {
        Task<RawUploadResult> UploadGpx(IFormFile file);
        Task<Models.Route> CreateDraftRouteAsync(string fileName, int userId, Stream fileStream);
        Task<bool> UpdateRouteAsync(RouteUpdateDto route);
        Task<List<Models.Route>> GetRoutes();
    }
}
