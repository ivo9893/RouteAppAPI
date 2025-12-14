using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace RouteAppAPI.Models
{

    [Table("DifficultyLevels")]
    public class DifficultyLevel
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public string? Description { get; set; }
        public int? SortOrder { get; set; }


        public ICollection<Route> Routes { get; set; } = [];
    }
}
