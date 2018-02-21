using System;
using System.Collections.Generic;
using System.Text;
using iqoption.core.data;
using iqoption.data.Model;
using Microsoft.EntityFrameworkCore;

namespace iqoption.data.Repositories
{
    public interface IPersonRepository
    {
        
    }

    public class PersonRepository : EfCoreRepositoryBase<Person>, IPersonRepository
    {
        public PersonRepository(iqOptionContext dbDbContext) : base(dbDbContext)
        {
        }
    }
}
