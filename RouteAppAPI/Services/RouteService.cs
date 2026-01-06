using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using GPXParser;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using RouteAppAPI.Data;
using RouteAppAPI.Models.DTO;
using RouteAppAPI.Services.Interfaces;

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
            using var memoryStream = new MemoryStream();
            await fileStream.CopyToAsync(memoryStream);

            memoryStream.Position = 0;

            var parsedFile = _parser.Parse(memoryStream);

            var coordinates = parsedFile
                .Points
                .Select(p => new Coordinate((double)p.Longitude, (double)p.Latitude))
                .ToArray();

            var routeName = parsedFile.Name;
            var totalDistance = parsedFile.TotalDistance;
            var totalElevation = parsedFile.TotalElevationGain;

            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            var lineString = geometryFactory.CreateLineString(coordinates);

            memoryStream.Position = 0;
            var uploadParams = new RawUploadParams
            {
                File = new FileDescription(fileName, memoryStream),
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
                CreatedAt = DateTime.UtcNow
            };

            _context.Routes.Add(draftRoute);
            await _context.SaveChangesAsync();

            return draftRoute;

        }

        public async Task<bool> UpdateRouteAsync(RouteUpdateDto route)
        {
            var existingRoute = await _context.Routes.FirstOrDefaultAsync(r => r.Id == route.Id);
            if (existingRoute == null)
            {
                return false;
            }
            existingRoute.Name = route.Name;
            existingRoute.Description = route.Description;
            existingRoute.RouteTypeId = route.RouteType;
            existingRoute.TerrainTypeId = route.TerrainType;
            existingRoute.DifficultyLevelId = route.DifficultyLevel;
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
