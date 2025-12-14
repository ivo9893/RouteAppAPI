using System;
using System.Collections.Generic;

namespace RouteAppAPI.Models.DTO
{
    public class UserStatsDto
    {
        public int TotalRoutes { get; set; }
        public decimal TotalDistanceKm { get; set; }
        public decimal TotalElevationGainM { get; set; }
        public int TotalLikesReceived { get; set; }
        public int FollowersCount { get; set; }
        public int FollowingCount { get; set; }
        public TerrainType? FavoriteTerrainType { get; set; }
        public DateTime MemberSince { get; set; }
    }

    public class RouteStatsDto
    {
        public decimal TotalDistanceKm { get; set; }
        public decimal AverageDistanceKm { get; set; }
        public decimal TotalElevationGainM { get; set; }
        public decimal AverageElevationGainM { get; set; }
        public int TotalRoutes { get; set; }
        public Dictionary<DifficultyLevel, int> RoutesByDifficulty { get; set; }
        public Dictionary<TerrainType, int> RoutesByTerrain { get; set; }
    }
}
