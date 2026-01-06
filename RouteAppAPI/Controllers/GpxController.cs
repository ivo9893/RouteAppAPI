using CloudinaryDotNet.Actions;
using GPXParser;
using Microsoft.AspNetCore.Mvc;
using RouteAppAPI.Models.DTO;
using RouteAppAPI.Services.Interfaces;

namespace RouteAppAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class GpxController : ControllerBase
    {

        private readonly IGpxParser _parser;
        private readonly IRouteService _routeService;

        public GpxController(IGpxParser parser, IRouteService routeService)
        {
            _parser = parser;
            _routeService = routeService;
        }


        [HttpPost("parse-gpx")]
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

        [HttpPost("upload-gpx")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadGPX(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No GPX file uploaded");
            }

            if(!file.FileName.EndsWith(".gpx", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("The file is not in .gpx format");
            }

            var response = await _routeService.UploadGpx(file);

            if(response == null)
            {
                return StatusCode(500, "Error uploading GPX file");
            }

            if(response.Error != null)
            {
                return BadRequest(ApiResponse<string>.Fail("Image upload failed", new List<string> { response.Error.Message }));
            }

            var gpxUrl = response.SecureUrl.ToString();



            return Ok(ApiResponse<string>.Ok(response.SecureUrl.ToString(), "GPX file was uploaded successfully!"));

        }


    }
}
