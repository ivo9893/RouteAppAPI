using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RouteAppAPI.Models
{
    [Table("SavedRoutes")]
    public class SavedRoute
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int RouteId { get; set; }

        public int? CollectionId { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("RouteId")]
        public virtual Route Route { get; set; }

        [ForeignKey("CollectionId")]
        public virtual Collection Collection { get; set; } = new Collection();
    }
}
