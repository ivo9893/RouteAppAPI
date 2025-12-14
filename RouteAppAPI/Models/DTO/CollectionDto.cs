using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RouteAppAPI.Models.DTO
{
    public class CollectionCreateDto
    {
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        public bool IsPublic { get; set; } = false;
    }

    public class CollectionUpdateDto
    {
        [StringLength(100, MinimumLength = 1)]
        public string? Name { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        public bool? IsPublic { get; set; }
    }

    public class CollectionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsPublic { get; set; }
        public int RoutesCount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Optional: preview routes
        public List<RouteListDto>? PreviewRoutes { get; set; }
    }
}
