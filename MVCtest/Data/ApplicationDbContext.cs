using Microsoft.EntityFrameworkCore;

namespace MVCtest.Data;

public class ApplicationDbContext:DbContext
{
    public
        ApplicationDbContext(DbContextOptions<ApplicationDbContext>
            options) : base(options)
    {
        
    }
}