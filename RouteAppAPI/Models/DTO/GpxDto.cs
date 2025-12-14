using System.Collections.Generic;

namespace RouteAppAPI.Models.DTO
{
    public class GpxUploadResponseDto
    {
        public string GpxFileUrl { get; set; }
        public decimal DistanceKm { get; set; }
        public decimal ElevationGainM { get; set; }
        public decimal ElevationLossM { get; set; }
        public decimal? MaxElevationM { get; set; }
        public decimal? MinElevationM { get; set; }
        public int TotalPoints { get; set; }
        public List<RoutePointDto> RoutePoints { get; set; }
    }
}
