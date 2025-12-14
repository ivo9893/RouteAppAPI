using System;

namespace RouteAppAPI.Models.DTO
{
    public class RoutePhotoDto
    {
        public int Id { get; set; }
        public string PhotoUrl { get; set; }
        public string? ThumbnailUrl { get; set; }
        public string? Caption { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsPrimary { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class PhotoUploadDto
    {
        public string? Caption { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsPrimary { get; set; }
    }
}
