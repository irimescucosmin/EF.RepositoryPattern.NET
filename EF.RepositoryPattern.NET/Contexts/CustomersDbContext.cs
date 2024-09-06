using EF.RepositoryPattern.NET.Entities;
using Microsoft.EntityFrameworkCore;

namespace EF.RepositoryPattern.NET.Contexts;

public class CustomersDbContext(DbContextOptions<CustomersDbContext> dbContextOptions) : DbContext(dbContextOptions)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CustomersEntity>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });
    }
}