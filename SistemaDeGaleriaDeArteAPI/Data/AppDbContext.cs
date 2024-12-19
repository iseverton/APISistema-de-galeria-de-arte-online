using Microsoft.EntityFrameworkCore;
using SistemaDeGaleriaDeArteAPI.Data.Mappings;
using SistemaDeGaleriaDeArteAPI.Models;

namespace SistemaDeGaleriaDeArteAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

   public DbSet<UserModel> Users { get; set; }
   public DbSet<WorkModel> Works { get; set; }
   public DbSet<CategoryModel> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserMap());
        modelBuilder.ApplyConfiguration(new WorkMap());
        modelBuilder.ApplyConfiguration(new CategoryMap());
    }
}
