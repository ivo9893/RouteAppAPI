using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using GPXParser;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using RouteAppAPI.Data;
using RouteAppAPI.Models;
using RouteAppAPI.Models.DTO;
using RouteAppAPI.Services.Interfaces;
using System.Text.Json;

namespace RouteAppAPI.Services
{
    public class RouteService : IRouteService
    {
        private readonly Cloudinary _cloudinary;
        private readonly IGpxParser _parser;
        private readonly ApplicationDbContext _context;

        public RouteService(Cloudinary cloudinary, IGpxParser parser, ApplicationDbContext context)
        {
            _cloudinary = cloudinary;
            _parser = parser;
            _context = context;
        }

        public async Task<Models.Route> CreateDraftRouteAsync(string fileName, int userId, Stream fileStream)
        {

            var parsedFile = _parser.Parse(fileStream);

            var coordinates = parsedFile
                .Points
                .Select(p => new Coordinate((double)p.Longitude, (double)p.Latitude))
                .ToArray();

            var routeName = parsedFile.Name;
            var totalDistance = parsedFile.TotalDistance;
            var totalElevation = parsedFile.TotalElevationGain;
            var (city, country) = await GetLocationFromCoordinates(coordinates[0].Y, coordinates[0].X);

            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            var lineString = geometryFactory.CreateLineString(coordinates);

            if (fileStream.CanSeek)
            {
                fileStream.Position = 0;
            }

            var uploadParams = new RawUploadParams
            {
                File = new FileDescription(fileName, fileStream),
            };

            var cloudResult = await _cloudinary.UploadAsync(uploadParams);

            var draftRoute = new Models.Route
            {
                Name = routeName,
                UserId = userId,
                IsDraft = true,
                Path = lineString,
                DistanceKm = (decimal)totalDistance,
                ElevationGainM = (decimal)totalElevation,
                GpxFileUrl = cloudResult.SecureUrl.ToString(),
                Country = country,
                Region = city,
                CreatedAt = DateTime.UtcNow
            };

            _context.Routes.Add(draftRoute);
            await _context.SaveChangesAsync();

            return draftRoute;

        }

        private async Task<(string City, string Country)> GetLocationFromCoordinates(double lat, double lon)
        {
            using var client = new HttpClient();

            client.DefaultRequestHeaders.Add("User-Agent", "RouteApp/1.0 (ivo_stoqnov@mail.bg)");

            var url = $"https://nominatim.openstreetmap.org/reverse?format=json&lat={lat}&lon={lon}";

            try
            {
                var responseString = await client.GetStringAsync(url);

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var result = JsonSerializer.Deserialize<NominatimResponse>(responseString, options);

                if (result?.Address != null)
                {
                    string city = result.Address.City
                                  ?? result.Address.Town
                                  ?? result.Address.Village
                                  ?? "Unknown Location";

                    return (city, result.Address.Country ?? "Unknown Country");
                }
            }
            catch (Exception ex)
            {
                // Log the error
                Console.WriteLine($"Geocoding failed: {ex.Message}");
            }

            return ("Unknown", "Unknown");
        }

        public async Task<List<Models.Route>> GetRoutes()
        {
            var allRoutes = await _context.Routes
                .Include(r => r.DifficultyLevel)
                .Include(u => u.User)
                .Where(r => r.IsDraft == false).ToListAsync();
            return allRoutes;
        }
        public async Task<List<Models.Route>> GetRoutesByUserId(int userId)
        {
            var allRoutes = await _context.Routes
                .Include(r => r.DifficultyLevel)
                .Include(u => u.User)
                .Where(r => r.IsDraft == false && r.UserId == userId).ToListAsync();
            return allRoutes;
        }

        public async Task<bool> UpdateRouteAsync(RouteUpdateDto route, int userId)
        {
            var existingRoute = await _context.Routes.FirstOrDefaultAsync(r => r.Id == route.Id && r.UserId == userId);
            if (existingRoute == null)
            {
                return false;
            }
            existingRoute.Name = route.Name ?? "";
            existingRoute.Description = route.Description;
            existingRoute.RouteTypeId = route.RouteType;
            existingRoute.TerrainTypeId = route.TerrainType;
            existingRoute.DifficultyLevelId = route.DifficultyLevel;
            existingRoute.IsDraft = false;
            if (route.ImagesUrls != null && route.ImagesUrls.Count > 0)
                existingRoute.ThumbnailUrl = route.ImagesUrls[0];

            _context.Routes.Update(existingRoute);
            _context.SaveChanges();
            return true;

        }

        public Task<RawUploadResult> UploadGpx(IFormFile file)
        {

            var uploadParams = new RawUploadParams
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
            };

            return _cloudinary.UploadAsync(uploadParams);
        }

        
    }
}
