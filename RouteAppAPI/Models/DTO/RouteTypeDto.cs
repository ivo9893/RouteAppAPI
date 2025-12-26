using System.ComponentModel.DataAnnotations;

namespace RouteAppAPI.Models.DTO
{
    public class RouteTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
