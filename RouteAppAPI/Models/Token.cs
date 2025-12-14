using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RouteAppAPI.Models
{

    [Table("Tokens")]
    public class Token
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }
        [Required]
        [MaxLength(255)]
        public string RefreshToken { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? RevokedAt { get; set; }    

        public bool IsRevoked => RevokedAt != null;

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
