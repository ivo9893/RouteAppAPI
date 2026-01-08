using System;

namespace RouteAppAPI.Models.DTO
{
    public class RoutePhotoDto
    {
        public int Id { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
