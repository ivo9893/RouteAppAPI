using Microsoft.AspNetCore.Mvc;
using RouteAppAPI.Services.Interfaces;

namespace RouteAppAPI.Controllers
{



    [ApiController]
    [Route("api/difficulty-level")]
    public class DifficultyLevelController : ControllerBase
    {

        private readonly IDifficultyLevelService _service;

        public DifficultyLevelController(IDifficultyLevelService service)
        {
            _service = service;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var difficultyLevels = await _service.GetDifficultyLevelsAsync();
            return Ok(difficultyLevels);
        }

        [HttpGet("{level}")]
        public async Task<IActionResult> GetDifficultyLevel(int level)
        {
            var difficultyLevel = await _service.GetDifficultyLevelAsync(level);

            if(difficultyLevel is null)
            {
                return NotFound();
            }

            return Ok(difficultyLevel);

        }

    }
}
