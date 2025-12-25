using RouteAppAPI.Models;

namespace RouteAppAPI.Services.Interfaces
{
    public interface IDifficultyLevelService
    {
        Task<List<DifficultyLevel>> GetDifficultyLevelsAsync();
        Task<DifficultyLevel?> GetDifficultyLevelAsync(int level);
    }
}
