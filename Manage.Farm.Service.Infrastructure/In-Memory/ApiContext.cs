using Manage.Farm.Service.Domain.Animal;
using Microsoft.EntityFrameworkCore;

namespace Manage.Farm.Service.Infrastructure.Dapper;

public class ApiContext : DbContext
{
    protected override void OnConfiguring
        (DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(databaseName: "FarmDb");
    }

    public DbSet<Animal> Animals { get; set; }
}