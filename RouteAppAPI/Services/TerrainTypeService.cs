using Microsoft.EntityFrameworkCore;
using RouteAppAPI.Data;
using RouteAppAPI.Models;
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


        public async Task<List<TerrainType>> GetTerrainTypes()
        {
            return await _context.TerrainTypes.ToListAsync();
        }
        public async Task<TerrainType?> GetTerrainTypeById(int id)
        {
            return await _context.TerrainTypes
                .FindAsync(id);
        }
    }
}
