using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace iqoption.data.Services
{

    public interface ISqlWrapper {

        #region WriteModel

        Task<int> ExecuteAsync(string sql);
        Task<int> ExecuteAsync(string sql, object prameter);

        #endregion

        #region [ReadModel]

        
        Task<IEnumerable<TResult>> QueryAsync<TResult>(string sql);
        Task<IEnumerable<TResult>> QueryAsync<TResult>(string sql, object parameters);


        #endregion
    }

    public class SqlWrapper : ISqlWrapper {

        private readonly Func<IDbConnection> _connFunc;


        public SqlWrapper(Func<IDbConnection> connFunc) {
            _connFunc = connFunc;
        }

        public Task<int> ExecuteAsync(string sql) {
            return _connFunc().ExecuteAsync(sql);
        }

        public Task<int> ExecuteAsync(string sql, object prameter) {
            return _connFunc().ExecuteAsync(sql, prameter);
        }

        public Task<IEnumerable<TResult>> QueryAsync<TResult>(string sql) {
            return _connFunc().QueryAsync<TResult>(sql);
        }

        public Task<IEnumerable<TResult>> QueryAsync<TResult>(string sql, object parameters) {
            return _connFunc().QueryAsync<TResult>(sql, parameters);
        }

    }
}
