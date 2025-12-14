using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RouteAppAPI.Models
{

    [Table("RoutePoints")]
    public class RoutePoints
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int RouteId { get; set; }
        [Required]
        [Column(TypeName = "decimal(10, 7)")]
        public decimal Latitude { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 7)")]
        public decimal Longitude { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal ElevationM { get; set; }

        [Required]
        public int SequenceOrder { get; set; }
        
        public DateTime Timestamp { get; set; }

        [ForeignKey("RouteId")]
        public virtual Route Route { get; set; } 

    }
}
