using Microsoft.AspNetCore.Mvc;
using RouteAppAPI.Services.Interfaces;

namespace RouteAppAPI.Controllers
{
    [ApiController]
    [Route("api/route-type")]
    public class RouteTypeController : ControllerBase
    {
        private readonly IRouteTypeService _routeTypeService;

        public RouteTypeController(IRouteTypeService routeTypeService)
        {
            _routeTypeService = routeTypeService;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var routeTypes = await _routeTypeService.GetRouteTypesAsync();
            return Ok(routeTypes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTypeById(int id)
        {
            var routeType = await _routeTypeService.GetRouteTypeAsync(id);

            if (routeType is null)
            {
                return NotFound();
            }
            return Ok(routeType);
        }
    }
}
