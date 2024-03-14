// EntityController.cs

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/entities")]
public class EntityController : ControllerBase
{
    private readonly IEntityService _entityService;

    public EntityController(IEntityService entityService)
    {
        _entityService = entityService;
    }

    [HttpPost]
    public async Task<ActionResult<Entity>> CreateEntity(Entity entity)
    {
        try
        {
            var createdEntity = await _entityService.CreateEntity(entity);
            return Ok(createdEntity);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Entity>> GetEntityById(string id)
    {
        try
        {
            var entity = await _entityService.GetEntityById(id);
            if (entity == null)
                return NotFound();

            return Ok(entity);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("search")]
    public async Task<ActionResult<List<Entity>>> SearchEntities(string searchTerm)
    {
        try
        {
            var entities = await _entityService.SearchEntities(searchTerm);
            return Ok(entities);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("filter")]
    public async Task<ActionResult<List<Entity>>> FilterEntities(string gender, DateTime? startDate, DateTime? endDate, List<string> countries)
    {
        try
        {
            var entities = await _entityService.FilterEntities(gender, startDate, endDate, countries);
            return Ok(entities);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Entity>> UpdateEntity(string id, Entity entity)
    {
        try
        {
            var updatedEntity = await _entityService.UpdateEntity(id, entity);
            if (updatedEntity == null)
                return NotFound();

            return Ok(updatedEntity);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteEntity(string id)
    {
        try
        {
            var result = await _entityService.DeleteEntity(id);
            if (!result)
                return NotFound();

            return Ok(true);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}