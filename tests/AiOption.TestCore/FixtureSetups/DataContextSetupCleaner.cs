using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

using Dapper;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AiOption.TestCore.FixtureSetups {

    public class DataContextSetupCleaner<T> 
        where T : DbContext, new() {
        
        public DataContextSetupCleaner() {

            //setup db
            SetupDbContext();
        }


        public void SetupDbContext() {

            using (var db = new T()) {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                db.Database.Migrate();

            }
        }

    }

}