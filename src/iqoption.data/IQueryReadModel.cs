using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace iqoption.data
{


    public interface IQueryReadModel<T> {
        Task<IEnumerable<T>> QueryAsync();
        Task<IEnumerable<T>> QueryAsync(string sql);
    }
}