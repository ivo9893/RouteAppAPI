using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RouteAppAPI.Models
{
    [Table("Follows")]
    public class Follow
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int FollowerUserId { get; set; }
        [Required]
        public int FollowedUserId { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }

        [ForeignKey("FollowerUserId")]
        public virtual User Follower { get; set; }

        [ForeignKey("FollowedUserId")]
        public virtual User Followed { get; set; }
    }
}
