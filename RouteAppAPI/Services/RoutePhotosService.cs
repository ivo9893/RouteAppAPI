using CloudinaryDotNet;
using RouteAppAPI.Data;
using RouteAppAPI.Services.Interfaces;

namespace RouteAppAPI.Services
{
    public class RoutePhotosService : IRoutePhotosService
    {

        private readonly Cloudinary _cloudinary;
        private readonly ApplicationDbContext _context;

        public RoutePhotosService(Cloudinary cloudinary, ApplicationDbContext context)
        {
            _cloudinary = cloudinary;
            _context = context;
        }

        public Task<bool> UploadRoutePhotos(List<IFormFile> photos)
        {


            return null;
        }
    }
}
