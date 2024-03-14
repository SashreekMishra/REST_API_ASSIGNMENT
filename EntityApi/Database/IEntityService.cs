using System.Collections.Generic;
using System.Threading.Tasks;

public interface IEntityService 
{
    Task<Entity> CreateEntity(Entity entity);
    Task<Entity> GetEntityById(string id);
    Task<List<Entity>> GetEntities();
    Task<List<Entity>> SearchEntities(string searchTerm);
    Task<List<Entity>> FilterEntities(string gender, DateTime? startDate, DateTime? endDate, List<string> countries);
    Task<Entity> UpdateEntity(string id, Entity entity);
    Task<bool> DeleteEntity(string id);
}