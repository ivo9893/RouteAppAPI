using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RouteAppAPI.Models.DTO
{
    public class RouteCreateDto
    {
        [Required]
        [StringLength(200, MinimumLength = 3)]
        public string Name { get; set; }

        [StringLength(2000)]
        public string? Description { get; set; }

        [StringLength(200)]
        public string? StartLocationName { get; set; }

        public decimal? StartLatitude { get; set; }
        public decimal? StartLongitude { get; set; }

        [StringLength(100)]
        public string? Region { get; set; }

        [StringLength(100)]
        public string? Country { get; set; }

        // These will be auto-calculated from GPX
        public decimal DistanceKm { get; set; }
        public decimal ElevationGainM { get; set; }
        public decimal ElevationLossM { get; set; }
        public decimal? MaxElevationM { get; set; }
        public decimal? MinElevationM { get; set; }

        [Required]
        public RouteType RouteType { get; set; }

        [Required]
        public TerrainType TerrainType { get; set; }

        [Required]
        public DifficultyLevel DifficultyLevel { get; set; }

        [StringLength(500)]
        public string? ParkingInfo { get; set; }

        public bool WaterAvailable { get; set; }

        [StringLength(100)]
        public string? BestSeasons { get; set; }

        public bool IsPublic { get; set; } = true;

        // Tags
        public List<string>? Tags { get; set; }
    }

    public class RouteUpdateDto
    {
        [Required]
        public int Id { get; set; }

        [StringLength(200, MinimumLength = 3)]
        public string? Name { get; set; }

        [StringLength(2000)]
        public string? Description { get; set; }

        public int? RouteType { get; set; }
        public int? TerrainType { get; set; }
        public int? DifficultyLevel { get; set; }

        public List<string> ImagesUrls { get; set; }

    }

    public class RouteListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal DistanceKm { get; set; }
        public decimal ElevationGainM { get; set; }
        public RouteType RouteType { get; set; }
        public TerrainType TerrainType { get; set; }
        public DifficultyLevel DifficultyLevel { get; set; }
        public string? PrimaryPhotoUrl { get; set; }
        public string? Region { get; set; }
        public int LikesCount { get; set; }
        public int CommentsCount { get; set; }
        public int SavesCount { get; set; }
        public DateTime CreatedAt { get; set; }

        // User info
        public int UserId { get; set; }
        public string Username { get; set; }
        public string? UserProfilePhotoUrl { get; set; }
    }

    public class RouteDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public string? StartLocationName { get; set; }
        public decimal? StartLatitude { get; set; }
        public decimal? StartLongitude { get; set; }
        public string? Region { get; set; }
        public string? Country { get; set; }

        public decimal DistanceKm { get; set; }
        public decimal ElevationGainM { get; set; }
        public decimal ElevationLossM { get; set; }
        public decimal? MaxElevationM { get; set; }
        public decimal? MinElevationM { get; set; }

        public RouteType RouteType { get; set; }
        public TerrainType TerrainType { get; set; }
        public DifficultyLevel DifficultyLevel { get; set; }

        public string? ParkingInfo { get; set; }
        public bool WaterAvailable { get; set; }
        public string? BestSeasons { get; set; }

        public string? GpxFileUrl { get; set; }

        public int LikesCount { get; set; }
        public int CommentsCount { get; set; }
        public int SavesCount { get; set; }
        public int ViewsCount { get; set; }

        public bool IsPublic { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // User info
        public UserProfileDto User { get; set; }

        // Photos
        public List<RoutePhotoDto> Photos { get; set; }

        // Tags
        public List<string> Tags { get; set; }

        // Current user's interaction
        public bool IsLikedByCurrentUser { get; set; }
        public bool IsSavedByCurrentUser { get; set; }
    }

    public class RoutePointDto
    {
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public decimal? ElevationM { get; set; }
        public int SequenceOrder { get; set; }
        public DateTime? Timestamp { get; set; }
    }
}
