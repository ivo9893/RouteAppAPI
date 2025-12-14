using System;
using System.Collections.Generic;

namespace RouteAppAPI.Models.DTO
{
    public class RouteSearchDto
    {
        public string? SearchTerm { get; set; }
        public decimal? MinDistanceKm { get; set; }
        public decimal? MaxDistanceKm { get; set; }
        public decimal? MinElevationGainM { get; set; }
        public decimal? MaxElevationGainM { get; set; }
        public List<RouteType>? RouteTypes { get; set; }
        public List<TerrainType>? TerrainTypes { get; set; }
        public List<DifficultyLevel>? DifficultyLevels { get; set; }
        public string? Region { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public decimal? RadiusKm { get; set; }
        public List<string>? Tags { get; set; }

        // Pagination
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;

        // Sorting
        public string SortBy { get; set; } = "CreatedAt"; // CreatedAt, Distance, Elevation, Likes, etc.
        public string SortOrder { get; set; } = "desc"; // asc, desc
    }

    public class PaginatedResultDto<T>
    {
        public List<T> Items { get; set; }
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
        public bool HasNextPage => Page < TotalPages;
        public bool HasPreviousPage => Page > 1;
    }
}
