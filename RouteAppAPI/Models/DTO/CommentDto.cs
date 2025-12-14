using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RouteAppAPI.Models.DTO
{
    public class CommentCreateDto
    {
        [Required]
        [StringLength(1000, MinimumLength = 1)]
        public string CommentText { get; set; }

        public int? ParentCommentId { get; set; }
    }

    public class CommentDto
    {
        public int Id { get; set; }
        public string CommentText { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int? ParentCommentId { get; set; }

        // User info
        public int UserId { get; set; }
        public string Username { get; set; }
        public string? UserProfilePhotoUrl { get; set; }

        // Nested replies (optional)
        public List<CommentDto>? Replies { get; set; }
    }
}
