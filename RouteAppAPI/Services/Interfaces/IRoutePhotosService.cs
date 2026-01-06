namespace RouteAppAPI.Services.Interfaces
{
    public interface IRoutePhotosService
    {

        Task<bool> UploadRoutePhotos(List<IFormFile> photos);
    }
}
