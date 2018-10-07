using Microsoft.EntityFrameworkCore;

namespace AiOption.TestCore.FixtureSetups
{
    public class DataContextSetupCleaner<T>
        where T : DbContext, new()
    {
        public DataContextSetupCleaner()
        {
            //setup db
            SetupDbContext();
        }


        public void SetupDbContext()
        {
            using (var db = new T())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                db.Database.Migrate();
            }
        }
    }
}