using iqoption.data.IqOptionAccount;
using iqoption.data.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace iqoption.data {
    public class AiOptionContext : IdentityDbContext<UserDto> {
        public AiOptionContext(DbContextOptions<AiOptionContext> options) : base(options) {
        }

        public DbSet<IqOptionAccountDto> IqOptionUsers { get; set; }


       
    }
}