using RouteAppAPI.Models;

namespace RouteAppAPI.Services.Interfaces
{
    public interface ITerrainTypeService
    {
        Task<List<TerrainType>> GetTerrainTypes();
        Task<TerrainType?> GetTerrainTypeById(int id);
    }
}
