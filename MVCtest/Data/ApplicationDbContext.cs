using Microsoft.EntityFrameworkCore;
using MVCtest.Models;

namespace MVCtest.Data;

public class ApplicationDbContext:DbContext
{
    public
        ApplicationDbContext(DbContextOptions<ApplicationDbContext>
            options) : base(options)
    {
        
    }
    public DbSet<Category>Categories { get; set; }
    public DbSet<denny>Denny { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "tea", DisplayOrder = 1 },
            new Category { Id = 2, Name = "fruits tea", DisplayOrder = 2 },
            new Category { Id = 3, Name = "coffee", DisplayOrder = 3 }
        );
        modelBuilder.Entity<denny>().HasData(
            new denny{Id = 1,Name = "denny",age = 25,DisplayOrder = 1}
            );

    }
}