using System;
using System.Data;

using Microsoft.EntityFrameworkCore;

namespace AiOption.TestCore.FixtureSetups {

    public class DataContextSetupCleaner<T> : IDisposable
        where T : DbContext, new()
    {

        public  T DataContext { get; }
        public IDbConnection DbConnection { get; private set; }

        public DataContextSetupCleaner()
        {

            //setup db
            DataContext = SetupDbContext();
        }


        public T SetupDbContext(T db = null) 
        {

            if (db == null)
            {
                db = new T();
            }

            db.Database.EnsureDeleted();
            db.Database.Migrate();
            db.Database.EnsureCreated();

            DbConnection = db.Database.GetDbConnection();

            return db;
        }

        public void Dispose()
        {
            DataContext.Dispose();
        }
    }

}