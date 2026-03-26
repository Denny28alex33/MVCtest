using Microsoft.EntityFrameworkCore.Diagnostics;
using MVCtest.DataAccess.Data;
using MVCtest.DataAccess.Repository.IRepository;

namespace MVCtest.DataAccess.Repository;

public class UnitOfWork :IUnitOfWork
{
    private ApplicationDbContext _db;
    public ICategoryRepository Category { get; private set; }

    public UnitOfWork(ApplicationDbContext db)
    {
        _db = db;
        Category = new CategoryRepository(_db);
    }
    public void save()
    {
        _db.SaveChanges();
    }
}