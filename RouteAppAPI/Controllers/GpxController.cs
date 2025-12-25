using GPXParser;
using Microsoft.AspNetCore.Mvc;

namespace RouteAppAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class GpxController : ControllerBase
    {

        private readonly IGpxParser _parser;

        public GpxController(IGpxParser parser)
        {
            _parser = parser;
        }


        [HttpPost("upload-gpx")]
        [Consumes("multipart/form-data")]

        public async Task<IActionResult> ParseGPX(IFormFile file)
        {
            if(file == null || file.Length == 0)
            {
                return BadRequest("No GPX file uploaded");
            }

            if (!file.FileName.EndsWith(".gpx", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("The files is not in .gpx format");
            }

            await using var stream = file.OpenReadStream();

            var parsedData = _parser.Parse(stream);

            return Ok(parsedData);
        }
    }
}
