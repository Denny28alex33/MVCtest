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

public class ProductRepository : Repository<Product>,IProductRepository
{
    private ApplicationDbContext _db;

    public ProductRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public void Update(Product obj)
    {
        var objFromDb = _db.Products.FirstOrDefault(u => u.ID == obj.ID);
        if (objFromDb!=null)
        {
            objFromDb.Name = obj.Name;
            objFromDb.Size = obj.Size;
            objFromDb.Price = obj.Price;
            objFromDb.Description = obj.Description;
            objFromDb.CategoryId = obj.CategoryId;
            if (objFromDb.ImageUrl != null)
            {
                objFromDb.ImageUrl = obj.ImageUrl;
            }
            
        }
    }

    
}

