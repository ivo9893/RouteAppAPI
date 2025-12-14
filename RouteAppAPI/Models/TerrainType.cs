using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace RouteAppAPI.Models
{

    [Table("TerrainTypes")]
    public class TerrainType
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public string? Description { get; set; }

        public ICollection<Route> Routes { get; set; } = [];
    }
}
