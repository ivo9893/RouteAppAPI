using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace RouteAppAPI.Models
{
    [Table("RouteTypes")]
    public class RouteType
    {
        [Required]
        [DatabaseGenerated (DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }  
        public string? Description { get; set; }

        // Navigation property
        public ICollection<Route> Routes { get; set; } = [];
    }
}
