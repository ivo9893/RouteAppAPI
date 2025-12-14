using System.Diagnostics;

namespace RouteAppAPI.Models
{
    public class DifficultyLevel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int? SortOrder { get; set; }  
        public ICollection<Route> Routes{ get; set; }
    }
}
