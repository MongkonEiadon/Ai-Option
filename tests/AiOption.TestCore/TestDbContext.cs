using Microsoft.EntityFrameworkCore;

namespace AiOption.TestCore {

    public class OneTimeSetupDataContextBase {

    }


    public class TestDbContext : DbContext {

        public TestDbContext() {

        }

        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options) {
        }

    }

}