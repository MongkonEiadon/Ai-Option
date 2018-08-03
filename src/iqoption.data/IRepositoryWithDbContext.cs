using Microsoft.EntityFrameworkCore;

namespace iqoption.data {
    public interface IRepositoryWithDbContext {
        DbContext GetDbContext();
    }
}