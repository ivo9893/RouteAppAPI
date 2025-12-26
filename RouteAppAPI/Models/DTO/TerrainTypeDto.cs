using System.ComponentModel.DataAnnotations;

namespace RouteAppAPI.Models.DTO
{
    public class TerrainTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

    }
}
