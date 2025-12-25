using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RouteAppAPI.Models
{
    [Table("Users")]
    public class User
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }
        [Required]
        [MaxLength(255)]
        public string PasswordHash { get; set; }

        [MaxLength(50)]
        public string? FirstName { get; set; }
        [MaxLength(50)]
        public string? LastName { get; set; }

        [MaxLength(2000)]
        public string? Bio { get; set; }
        [MaxLength(255)]

        public string? ProfilePhotoUrl { get; set; }

        [MaxLength(100)]
        public string? Location { get; set; }

        [Column(TypeName = "int")]
        public int TotalRoutes { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalDistanceKm { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalElevationGainM { get; set; }


        public bool IsEmailVerified { get; set; } = false;
        public bool IsActive { get; set; } = true;


        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public DateTime UpdatedAt { get; set; }
        public DateTime? LastLoginAt { get; set; }

        public virtual ICollection<Route> Routes { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<SavedRoute> SavedRoutes { get; set; }
        public virtual ICollection<Collection> Collections { get; set; }
        public virtual ICollection<Follow> Following { get; set; } = new List<Follow>();
        public virtual ICollection<Follow> Followers { get; set; } = new List<Follow>();
    }
}
