using Microsoft.EntityFrameworkCore;
using RouteAppAPI.Data;
using RouteAppAPI.Models;
using RouteAppAPI.Models.DTO;
using RouteAppAPI.Services.Interfaces;

namespace RouteAppAPI.Services
{
    public class TerrainTypeService : ITerrainTypeService
    {

        private readonly ApplicationDbContext _context;

        public TerrainTypeService(ApplicationDbContext context)
        {
            this._context = context;
        }


        public async Task<List<TerrainTypeDto>> GetTerrainTypes()
        {
            return await _context.TerrainTypes.Select(tt => new TerrainTypeDto
            {
                Id = tt.Id,
                Name = tt.Name,
                Description = tt.Description
            }).ToListAsync();
        }
        public async Task<TerrainTypeDto?> GetTerrainTypeById(int id)
        {
            var entity = await _context.TerrainTypes
                .FindAsync(id);

            if (entity == null) return null;

            return new TerrainTypeDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description
            };
        }
    }
}
