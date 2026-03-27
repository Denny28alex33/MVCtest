using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MVCtest.DataAccess.Data;
using MVCtest.DataAccess.Repository.IRepository;
using MVCtest.Models;

namespace MVCtest.DataAccess.Repository;

public class ProdustRepository : Repository<Category>, ICategoryRepository
{
    private ApplicationDbContext _db;

    public ProdustRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public void Update(Category obj)
    {
        _db.Categories.Update(obj);
    }

    
}

