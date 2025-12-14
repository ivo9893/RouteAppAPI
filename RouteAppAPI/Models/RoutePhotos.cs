using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RouteAppAPI.Models
{

    [Table("RoutePhotos")]
    public class RoutePhotos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int RouteId { get; set; }

        [Required]
        public int UserId { get; set; }
        [Required]
        [MaxLength(500)]
        public string PhotoUrl { get; set; }

        [MaxLength(500)]
        public string? ThumbnailUrl { get; set; }

        [MaxLength(500)]
        public string? Caption { get; set; }

        [Column(TypeName = "decimal(10, 7)")]
        public decimal? Latitude { get; set; }

        [Column(TypeName = "decimal(10, 7)")]
        public decimal? Longitude { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("RouteId")]
        public virtual Route Route { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
