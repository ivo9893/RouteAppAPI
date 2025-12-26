using Microsoft.EntityFrameworkCore;
using RouteAppAPI.Data;
using RouteAppAPI.Models.DTO;
using RouteAppAPI.Services.Interfaces;

namespace RouteAppAPI.Services
{
    public class RouteTypeService : IRouteTypeService
    {
        private readonly ApplicationDbContext _context;

        public RouteTypeService(ApplicationDbContext context) {
            _context = context; 
        }

        public async Task<RouteTypeDto?> GetRouteTypeAsync(int id)
        {
            var entity = await _context.RouteTypes.FindAsync(id);

            if (entity == null) { return null; }

            return new RouteTypeDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description
            };
        }

        public async Task<List<RouteTypeDto>> GetRouteTypesAsync()
        {
            return await _context.RouteTypes.Select(rt => new RouteTypeDto
            {
                Id = rt.Id,
                Name = rt.Name,
                Description = rt.Description
            }).ToListAsync();
        }
    }
}
