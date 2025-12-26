using Microsoft.EntityFrameworkCore;
using RouteAppAPI.Data;
using RouteAppAPI.Models;
using RouteAppAPI.Models.DTO;
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

        public async Task<DifficultyLevelDto?> GetDifficultyLevelAsync(int level)
        {
            var entity = await _context.DifficultyLevels.FindAsync(level);
            
            if (entity == null) { return null; }

            return new DifficultyLevelDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                SortOrder = entity.SortOrder
            };
        }

        public async Task<List<DifficultyLevelDto>> GetDifficultyLevelsAsync()
        {
            return await _context.DifficultyLevels.Select(dto => new DifficultyLevelDto
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                SortOrder = dto.SortOrder
            }).ToListAsync();
        }
    }
}
