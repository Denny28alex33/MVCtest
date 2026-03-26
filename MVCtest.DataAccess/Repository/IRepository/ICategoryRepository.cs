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

namespace MVCtest.DataAccess.Repository.IRepository;


public interface ICategoryRepository:IRepository<Category>
{
    void Update(Category obj);
    
}