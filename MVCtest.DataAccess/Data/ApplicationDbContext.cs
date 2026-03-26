using Microsoft.EntityFrameworkCore;
using MVCtest.Models;

namespace MVCtest.DataAccess.Data;

public class ApplicationDbContext:DbContext
{
    public
        ApplicationDbContext(DbContextOptions<ApplicationDbContext>
            options) : base(options)
    {
        
    }
    public DbSet<Category>Categories { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "tea", DisplayOrder = 1 },
            new Category { Id = 2, Name = "fruits tea", DisplayOrder = 2 },
            new Category { Id = 3, Name = "coffee", DisplayOrder = 3 }
        );
    }
}