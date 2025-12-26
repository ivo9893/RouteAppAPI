using RouteAppAPI.Models;
using RouteAppAPI.Models.DTO;

namespace RouteAppAPI.Services.Interfaces
{
    public interface ITerrainTypeService
    {
        Task<List<TerrainTypeDto>> GetTerrainTypes();
        Task<TerrainTypeDto?> GetTerrainTypeById(int id);
    }
}
