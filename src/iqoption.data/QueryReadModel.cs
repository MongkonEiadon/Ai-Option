using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace iqoption.data
{
    public class QueryReadModel<T>  : IQueryReadModel<T> {
        protected Func<IDbConnection> SqlConnection { get; }

        public QueryReadModel(Func<IDbConnection> sqlConnection) {
            SqlConnection = sqlConnection;
        }


        public Task<IEnumerable<T>> QueryAsync() {
            return SqlConnection().QueryAsync<T>($"select * from dbo.{nameof(T)}");
        }

        public Task<IEnumerable<T>> QueryAsync(string sql) {
            return SqlConnection().QueryAsync<T>(sql);
        }
    }
}