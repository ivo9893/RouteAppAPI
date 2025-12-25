using Microsoft.EntityFrameworkCore;
using RouteAppAPI.Data;
using RouteAppAPI.Models;
using RouteAppAPI.Services.Interfaces;

namespace RouteAppAPI.Services
{
    public class DifficultyLevelService : IDifficultyLevelService
    {
        private readonly ApplicationDbContext _context;

        public DifficultyLevelService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DifficultyLevel?> GetDifficultyLevelAsync(int level)
        {
            return await _context.DifficultyLevels.FindAsync(level);
        }

        public async Task<List<DifficultyLevel>> GetDifficultyLevelsAsync()
        {
            return await _context.DifficultyLevels.ToListAsync();
        }
    }
}
