using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVCtest.DataAccess.Repository;

namespace MVCtest.DataAccess.Repository;

public class Repository<T>:IRepository<T> where T:class
{
    
}