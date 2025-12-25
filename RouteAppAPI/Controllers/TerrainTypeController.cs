using Microsoft.AspNetCore.Mvc;
using RouteAppAPI.Services;
using RouteAppAPI.Services.Interfaces;

namespace RouteAppAPI.Controllers
{


    [ApiController]
    [Route("api/terrain-type")]
    public class TerrainTypeController : ControllerBase
    {

        private readonly ITerrainTypeService _service;

        public TerrainTypeController(ITerrainTypeService service)
        {
            _service = service;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var terrainTypes = await _service.GetTerrainTypes();
            return Ok(terrainTypes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTypeById(int id)
        {
            var terrainType = await _service.GetTerrainTypeById(id);

            if(terrainType is null)
            {
                return NotFound();
            }
            return Ok(terrainType);
        }
    }
}
