using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RouteAppAPI.Models
{

    [Table("Routes")]
    public class Route
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string? Description { get; set; }

        [MaxLength(200)]
        public string? StartLocationName { get; set; }

        [Column(TypeName = "decimal(10, 7")]
        public decimal? StartLatitude { get; set; }
        [Column(TypeName = "decimal(10, 7")]
        public decimal? StartLongitude { get; set; }

        [MaxLength(200)]
        public string? Region { get; set; }

        [MaxLength(100)]
        public string? Country { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal DistanceKm { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal ElevationGainM { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal ElevationLossM { get; set; }
        public decimal? MaxElevationM { get; set; }
        public decimal? MinElevationM { get; set; }


        [Required]
        public RouteType RouteType { get; set; }

        [Required]
        public TerrainType TerrainType { get; set; }

        [Required]
        public DifficultyLevel DifficultyLevel { get; set; }

        public bool WaterAvailable { get; set; } = false;

        [MaxLength(100)]
        public string? BestSeasons { get; set; }

        [MaxLength(255)]
        public string? GpxFileUrl { get; set; }

        public int LikesCount { get; set; } = 0;
        public int CommentsCount { get; set; } = 0;
        public int SavesCount { get; set; } = 0;
        public int ViewsCount { get; set; } = 0;

        public bool IsPublic { get; set; } = false;
        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public virtual ICollection<RoutePoints> RoutePoints { get; set; } = new List<RoutePoints>();
        public virtual ICollection<RoutePhotos> RoutePhotos { get; set; } = new List<RoutePhotos>();
        public virtual ICollection<Like> Likes { get; set; } = new List<Like>();
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public virtual ICollection<SavedRoute> SavedRoutes { get; set; } = new List<SavedRoute>();
        public virtual ICollection<RouteView> RouteViews { get; set; } = new List<RouteView>();
    }
}
