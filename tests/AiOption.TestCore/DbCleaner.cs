using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

using Dapper;

namespace AiOption.TestCore {

    public class DbCleaner
    {
        private static readonly string[] IGNORED_TABLES = { "sysdiagrams", "__MigrationHistory" };

        private static readonly object LOCK_OBJ = new object();

        private static string[] tablesToDelete;
        private static string deleteSql;
        private static bool initialized;
        
        private readonly IDbConnection _dbConnection;

        public DbCleaner(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;

            BuildDeleteTables();
        }

        public virtual Task DeleteAllData() {
            return _dbConnection.ExecuteAsync(deleteSql);
        }

        private class Relationship
        {
            public string PrimaryKeyTable { get; set; }
            public string ForeignKeyTable { get; set; }
        }

        private class Table
        {
            public string Name { get; set; }
        }

        private void BuildDeleteTables() {

            lock (LOCK_OBJ) {
                if (initialized)
                    return;

                tablesToDelete = GetSortedTables();
                deleteSql = BuildTableSql(tablesToDelete);

                initialized = true;
            }
        }

        private static string BuildTableSql(IEnumerable<string> tablesToDelete)
        {
            var sqls = tablesToDelete.Select(x => string.Format((string) "delete from [{0}]", (object) x)).ToArray();
            return string.Join(";", sqls);
        }

        private string[] GetSortedTables()
        {
            var allTables = GetAllTables().ToList();

            var allRelationships = GetRelationships().ToList();

            var sortedTables = new List<string>();

            while (allTables.Any())
            {
                var leafTables = allTables.Except(allRelationships.Select(rel => rel.PrimaryKeyTable)).ToArray();

                sortedTables.AddRange(leafTables);

                foreach (var leafTable in leafTables)
                {
                    allTables.Remove(leafTable);
                    var relToRemove = allRelationships.Where(rel => rel.ForeignKeyTable == leafTable).ToArray();
                    foreach (var rel in relToRemove)
                    {
                        allRelationships.Remove(rel);
                    }
                }
            }

            return sortedTables.ToArray();
        }

        private IEnumerable<Relationship> GetRelationships()
        {
            const string sql = @"
                        select
	                        so_pk.name as PrimaryKeyTable
                        ,   so_fk.name as ForeignKeyTable
                        from
	                        sysforeignkeys sfk
		                        inner join sysobjects so_pk on sfk.rkeyid = so_pk.id
		                        inner join sysobjects so_fk on sfk.fkeyid = so_fk.id
                        order by
	                        so_pk.name
                        ,   so_fk.name";

            return _dbConnection.Query<Relationship>(sql);
        }

        private IEnumerable<string> GetAllTables()
        {
            return 
                _dbConnection.Query<Table>("select Name from sys.tables")
                    .Select(x => x.Name)
                    .Except(IGNORED_TABLES)
                    .ToList();
        }
    }

}