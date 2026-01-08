using CloudinaryDotNet;
using RouteAppAPI.Data;
using RouteAppAPI.Models;
using RouteAppAPI.Services.Interfaces;

namespace RouteAppAPI.Services
{
    public class RoutePhotosService : IRoutePhotosService
    {

        private readonly ApplicationDbContext _context;

        public RoutePhotosService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> UploadRoutePhotos(List<RoutePhotos> photos)
        {
            if (photos == null || !photos.Any())
            {
                return false;
            }

            try
            {
                _context.RoutePhotos.AddRange(photos);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return false;
            }
            return true;
        }
    }
}
