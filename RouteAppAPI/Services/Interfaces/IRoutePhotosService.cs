using RouteAppAPI.Models;

namespace RouteAppAPI.Services.Interfaces
{
    public interface IRoutePhotosService
    {

        Task<bool> UploadRoutePhotos(List<RoutePhotos> photos);
    }
}
