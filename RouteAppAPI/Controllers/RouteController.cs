using Microsoft.AspNetCore.Mvc;
using RouteAppAPI.Models;
using RouteAppAPI.Models.DTO;
using RouteAppAPI.Services.Interfaces;
using System.Security.Claims;

namespace RouteAppAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RouteController : ControllerBase
    {
        private readonly IRouteService _routeService;
        private readonly IRoutePhotosService _routePhotoService;

        public RouteController(IRouteService routeService, IRoutePhotosService routePhotoService)
        {
            _routeService = routeService;
            _routePhotoService = routePhotoService;
        }


        [HttpGet("get-routes")]
        public async Task<ActionResult<ApiResponse<object>>> GetRoutes()
        {
            try
            {
                var routes = await _routeService.GetRoutes();

                var formattedRoutes = routes.Select(route => new
                {
                    routeId = route.Id,
                    name = route.Name,
                    distanceKm = route.DistanceKm,
                    elevationGainM = route.ElevationGainM,
                    isDraft = route.IsDraft,
                    createdAt = route.CreatedAt,
                    gpxFileUrl = route.GpxFileUrl,
                    user = new
                    {
                        id = route.User.Id,
                        name = route.User.FirstName + " " + route.User.LastName,
                        avatar = route.User.ProfilePhotoUrl
                    },
                    thumbnailUrl = route.ThumbnailUrl,
                    difficultyLevel = route.DifficultyLevel?.Name,
                    country = route.Country,
                    city = route.Region


                }).ToList();

                return ApiResponse<object>.Ok(formattedRoutes, "success");

            } catch (Exception e)
            {
                var errorResponse = ApiResponse<object>.Fail(
                    errorMessage: "An error occurred while processing your request.",
                    errors: new List<string> { e.Message } 
                );

                return StatusCode(500, errorResponse);
            }
            finally
            {

            }

        }

        [HttpPost("initial-create-route")]
        [Consumes("multipart/form-data")]

        public async Task<ActionResult<ApiResponse<object>>> InitialCreateRoute(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No GPX file uploaded");
            }

            if (!file.FileName.EndsWith(".gpx", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("The files is not in .gpx format");
            }

            if (User.Identity?.IsAuthenticated != true)
            {
                return Unauthorized("Unauthorized");
            }

            await using var stream = file.OpenReadStream();

            var routeCreated = await _routeService.CreateDraftRouteAsync(file.FileName, int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var id) ? id : 1, stream);

            return ApiResponse<object>.Ok(new
            {
                route = new
                {
                    routeId = routeCreated.Id,
                    totalDistance = routeCreated.DistanceKm,
                    totalElevation = routeCreated.ElevationGainM,
                    name = routeCreated.Name,
                    city = routeCreated.Region,
                    country = routeCreated.Country

                },
                message = "Route created successfully!"
            });
        }

        [HttpPut("update-route")]
        public async Task<ActionResult<ApiResponse<object>>> UpdateRoute([FromBody] RouteUpdateDto route)
        {

            if (User.Identity?.IsAuthenticated != true)
            {
                return Unauthorized("Unauthorized");
            }

            int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId);
            var updateResult = await _routeService.UpdateRouteAsync(route, userId);
            
            if (!updateResult)
            {
                return ApiResponse<object>.Fail("Route not found");
            }

            if (route.ImagesUrls != null && route.ImagesUrls.Any())
            {
                var photos = route.ImagesUrls.Select(url => new RoutePhotos
                {
                    RouteId = route.Id,
                    UserId = userId,
                    PhotoUrl = url,
                    CreatedAt = DateTime.UtcNow
                }).ToList();
                var photosResult = await _routePhotoService.UploadRoutePhotos(photos);
                if (!photosResult)
                {
                    return ApiResponse<object>.Fail("Failed to upload route photos");
                }
            }

            return ApiResponse<object>.Ok(new
            {
                message = "Route updated successfully!"
            });
        }
    }
}
