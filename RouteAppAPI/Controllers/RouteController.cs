using Microsoft.AspNetCore.Mvc;
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

        public RouteController(IRouteService routeService)
        {
            _routeService = routeService;
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

            await using var stream = file.OpenReadStream();

            var routeCreated = await _routeService.CreateDraftRouteAsync(file.FileName, int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value), stream);

            return ApiResponse<object>.Ok(new
            {
                route = new
                {
                    routeId = routeCreated.Id,
                    totalDistance = routeCreated.DistanceKm,
                    totalElevation = routeCreated.ElevationGainM,
                    name = routeCreated.Name
                },
                message = "Route created successfully!"
            });
        }

        [HttpPut("update-route")]
        public async Task<ActionResult<ApiResponse<object>>> UpdateRoute([FromBody] RouteUpdateDto route)
        {
            var updateResult = await _routeService.UpdateRouteAsync(route);
            if (!updateResult)
            {
                return ApiResponse<object>.Fail("Route not found");
            }
            return ApiResponse<object>.Ok(new
            {
                message = "Route updated successfully!"
            });
        }
    }
}
