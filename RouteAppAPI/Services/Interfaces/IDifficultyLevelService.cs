using RouteAppAPI.Models;
using RouteAppAPI.Models.DTO;

namespace RouteAppAPI.Services.Interfaces
{
    public interface IDifficultyLevelService
    {
        Task<List<DifficultyLevelDto>> GetDifficultyLevelsAsync();
        Task<DifficultyLevelDto?> GetDifficultyLevelAsync(int level);
    }
}
