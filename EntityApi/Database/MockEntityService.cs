using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public class MockEntityService : IEntityService 
{
    private const int MaxRetryAttempts = 3;
    private const int DelayBetweenRetriesMs = 1000; // 1 second

    private readonly List<Entity> _entities;

    public MockEntityService()
    {
        // Initialize with some mock data
        _entities = new List<Entity>
        {
            new Entity
            {
                Id = "1",
                Addresses = new List<Address>
                {
                    new Address { AddressLine = "123 Main St", City = "City", Country = "Country" }
                },
                Dates = new List<Date> { new Date { DateType = "Birth", DateValue = new DateTime(1990, 1, 1) } },
                Deceased = false,
                Gender = "Male",
                Names = new List<Name> { new Name { FirstName = "John", MiddleName = "Doe", Surname = "Smith" } }
            },
            // Add more mock entities as needed
        };
    }

    public async Task<Entity> CreateEntity(Entity entity)
    {
        int retryAttempt = 0;

        while (retryAttempt < MaxRetryAttempts)
        {
            try
            {
                // Simulate database write operation
                _entities.Add(entity);
                return await Task.FromResult(entity);
            }
            catch (Exception ex)
            {
                // Log the retry attempt and delay
                Console.WriteLine($"CreateEntity - Retry attempt #{retryAttempt + 1} failed. Retrying in {DelayBetweenRetriesMs}ms. Error: {ex.Message}");
                Thread.Sleep(DelayBetweenRetriesMs);
                retryAttempt++;
            }
        }

        // Log permanent failure if max attempts reached
        Console.WriteLine($"CreateEntity - Max retry attempts reached. Operation failed permanently.");
        return null;
    }

    public async Task<Entity> GetEntityById(string id)
    {
        // Implement retry mechanism for fetching entity by ID
        int retryAttempt = 0;

        while (retryAttempt < MaxRetryAttempts)
        {
            try
            {
                // Simulate database read operation
                var entity = _entities.FirstOrDefault(e => e.Id == id);
                if (entity != null)
                    return await Task.FromResult(entity);
                else
                    throw new Exception($"Entity with ID '{id}' not found.");
            }
            catch (Exception ex)
            {
                // Log the retry attempt and delay
                Console.WriteLine($"GetEntityById - Retry attempt #{retryAttempt + 1} failed. Retrying in {DelayBetweenRetriesMs}ms. Error: {ex.Message}");
                Thread.Sleep(DelayBetweenRetriesMs);
                retryAttempt++;
            }
        }

        // Log permanent failure if max attempts reached
        Console.WriteLine($"GetEntityById - Max retry attempts reached. Operation failed permanently.");
        return null;
    }

    public async Task<List<Entity>> GetEntities()
    {
        // Implement retry mechanism for fetching all entities
        int retryAttempt = 0;

        while (retryAttempt < MaxRetryAttempts)
        {
            try
            {
                // Simulate database read operation
                return await Task.FromResult(_entities.ToList());
            }
            catch (Exception ex)
            {
                // Log the retry attempt and delay
                Console.WriteLine($"GetEntities - Retry attempt #{retryAttempt + 1} failed. Retrying in {DelayBetweenRetriesMs}ms. Error: {ex.Message}");
                Thread.Sleep(DelayBetweenRetriesMs);
                retryAttempt++;
            }
        }

        // Log permanent failure if max attempts reached
        Console.WriteLine($"GetEntities - Max retry attempts reached. Operation failed permanently.");
        return null;
    }

    public async Task<List<Entity>> SearchEntities(string searchTerm)
    {
        // Implement retry mechanism for searching entities
        int retryAttempt = 0;

        while (retryAttempt < MaxRetryAttempts)
        {
            try
            {
                // Simulate database search operation
                var entities = _entities.Where(e => e.Names.Any(n =>
                    n.FirstName?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) == true ||
                    n.MiddleName?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) == true ||
                    n.Surname?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) == true ||
                    e.Addresses?.Any(a =>
                        a.AddressLine?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) == true ||
                        a.City?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) == true ||
                        a.Country?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) == true) == true
                )).ToList();
                return await Task.FromResult(entities);
            }
            catch (Exception ex)
            {
                // Log the retry attempt and delay
                Console.WriteLine($"SearchEntities - Retry attempt #{retryAttempt + 1} failed. Retrying in {DelayBetweenRetriesMs}ms. Error: {ex.Message}");
                Thread.Sleep(DelayBetweenRetriesMs);
                retryAttempt++;
            }
        }

        // Log permanent failure if max attempts reached
        Console.WriteLine($"SearchEntities - Max retry attempts reached. Operation failed permanently.");
        return null;
    }

    public async Task<List<Entity>> FilterEntities(string gender, DateTime? startDate, DateTime? endDate, List<string> countries)
    {
        // Implement retry mechanism for filtering entities
        int retryAttempt = 0;

        while (retryAttempt < MaxRetryAttempts)
        {
            try
            {
                // Simulate database filter operation
                var filteredEntities = _entities;

                if (!string.IsNullOrEmpty(gender))
                    filteredEntities = filteredEntities.Where(e => e.Gender == gender).ToList();

                if (startDate.HasValue)
                    filteredEntities = filteredEntities.Where(e => e.Dates.Any(d => d.DateValue >= startDate)).ToList();

                if (endDate.HasValue)
                    filteredEntities = filteredEntities.Where(e => e.Dates.Any(d => d.DateValue <= endDate)).ToList();

                if (countries != null && countries.Any())
                    filteredEntities = filteredEntities.Where(e => e.Addresses.Any(a => countries.Contains(a.Country))).ToList();

                return await Task.FromResult(filteredEntities);
            }
            catch (Exception ex)
            {
                // Log the retry attempt and delay
                Console.WriteLine($"FilterEntities - Retry attempt #{retryAttempt + 1} failed. Retrying in {DelayBetweenRetriesMs}ms. Error: {ex.Message}");
                Thread.Sleep(DelayBetweenRetriesMs);
                retryAttempt++;
            }
        }

        // Log permanent failure if max attempts reached
        Console.WriteLine($"FilterEntities - Max retry attempts reached. Operation failed permanently.");
        return null;
    }

    public async Task<Entity> UpdateEntity(string id, Entity entity)
    {
        // Implement retry mechanism for updating an entity
        int retryAttempt = 0;

        while (retryAttempt < MaxRetryAttempts)
        {
            try
            {
                // Simulate database update operation
                var existingEntity = await GetEntityById(id);
                if (existingEntity == null)
                    throw new Exception($"Entity with ID '{id}' not found.");

                // Update properties
                existingEntity.Addresses = entity.Addresses;
                existingEntity.Dates = entity.Dates;
                existingEntity.Deceased = entity.Deceased;
                existingEntity.Gender = entity.Gender;
                existingEntity.Names = entity.Names;

                return await Task.FromResult(existingEntity);
            }
            catch (Exception ex)
            {
                // Log the retry attempt and delay
                Console.WriteLine($"UpdateEntity - Retry attempt #{retryAttempt + 1} failed. Retrying in {DelayBetweenRetriesMs}ms. Error: {ex.Message}");
                Thread.Sleep(DelayBetweenRetriesMs);
                retryAttempt++;
            }
        }

        // Log permanent failure if max attempts reached
        Console.WriteLine($"UpdateEntity - Max retry attempts reached. Operation failed permanently.");
        return null;
    }

    public async Task<bool> DeleteEntity(string id)
    {
        // Implement retry mechanism for deleting an entity
        int retryAttempt = 0;

        while (retryAttempt < MaxRetryAttempts)
        {
            try
            {
                // Simulate database delete operation
                var entityToRemove = await GetEntityById(id);
                if (entityToRemove == null)
                    throw new Exception($"Entity with ID '{id}' not found.");

                _entities.Remove(entityToRemove);
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                // Log the retry attempt and delay
                Console.WriteLine($"DeleteEntity - Retry attempt #{retryAttempt + 1} failed. Retrying in {DelayBetweenRetriesMs}ms. Error: {ex.Message}");
                Thread.Sleep(DelayBetweenRetriesMs);
                retryAttempt++;
            }
        }

        // Log permanent failure if max attempts reached
        Console.WriteLine($"DeleteEntity - Max retry attempts reached. Operation failed permanently.");
        return false;
    }
}