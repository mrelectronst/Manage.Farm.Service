using Manage.Farm.Service.Domain.Animal;
using Microsoft.EntityFrameworkCore;

namespace Manage.Farm.Service.Infrastructure.Dapper;

public class AnimalService : IAnimalService
{
    public AnimalService()
    {
    }

    public async Task<List<Animal>> GetAll()
    {
        await using var context = new ApiContext();
        return await context.Animals.ToListAsync();
    }

    public async Task<Animal> Get(string name)
    {
        await using var context = new ApiContext();
        return await context.Animals.FindAsync(name);
    }

    public async Task<int> Add(Animal obj)
    {
        using var context = new ApiContext();
        await context.Animals.AddAsync(obj);
        return await context.SaveChangesAsync();
    }

    public async Task<int> Delete(Guid id)
    {
        await using var context = new ApiContext();
        var entity = await context.Animals.FindAsync(id);
        if (entity == null) return default;
        context.Animals.Remove(entity);
        return await context.SaveChangesAsync();
    }
}