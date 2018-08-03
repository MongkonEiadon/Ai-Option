using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Dapper;
using iqoption.data.IqOptionAccount;
using iqoption.domain.IqOption;
using iqoption.domain.Users;

namespace iqoption.data.Services.ReadModelServices {
    public interface IIqAccountQueryService {
        Task<IEnumerable<IqAccount>> GetActiveIqAccountsAsync(CancellationToken ctx);
        Task<IEnumerable<IqAccount>> GetInActiveIqAccountsAsync(CancellationToken ctx);
        Task<Dictionary<string, IEnumerable<IqAccount>>> GetLevelIqAccountCollectionsAsync(CancellationToken ctx);
    }

    public class IqAccountQueryService : QueryReadModel<IqOptionAccountDto>, IIqAccountQueryService {
        private readonly IMapper _mapper;

        public IqAccountQueryService(Func<IDbConnection> sqlConnection, IMapper mapper) : base(sqlConnection) {
            _mapper = mapper;
        }

      
        public Task<IEnumerable<IqAccount>> GetActiveIqAccountsAsync(CancellationToken ctx) {
            return SqlConnection()
                .QueryAsync(@"
                        SELECT iq.*, (select top 1 _r.Name from dbo.AspNetUserRoles _ur 
                                                inner join dbo.AspNetRoles _r on _ur.RoleId = _r.Id
                                                where iq.UserId = _ur.UserId) as Level
                        FROM [aioptiondb].[dbo].[IqOptionAccount] iq 
                        WHERE iq.IsActive = '1'")
                .ContinueWith(t => _mapper.Map<IEnumerable<IqAccount>>(t.Result), ctx);
        }

        public Task<IEnumerable<IqAccount>> GetInActiveIqAccountsAsync(CancellationToken ctx) {
            return SqlConnection()
                .QueryAsync($"Select *, 'n/a' as Level from dbo.IqOptionAccount where {nameof(IqOptionAccountDto.IsActive)} = '0'")
                .ContinueWith(t => _mapper.Map<IEnumerable<IqAccount>>(t.Result), ctx);
        }

        public Task<Dictionary<string, IEnumerable<IqAccount>>> GetLevelIqAccountCollectionsAsync(CancellationToken ctx) {
            var tcs = new TaskCompletionSource<Dictionary<string, IEnumerable<IqAccount>>>();

            try
            {
                var query = @"SELECT iq.*, (select top 1 _r.Name from dbo.AspNetUserRoles _ur 
                                inner join dbo.AspNetRoles _r on _ur.RoleId = _r.Id
                                where iq.UserId = _ur.UserId) as Level
                      FROM [aioptiondb].[dbo].[IqOptionAccount] iq 
                      ";


                var queryResult = SqlConnection().QueryAsync<IqAccount>(query);

                queryResult.ContinueWith(t =>
                {
                    foreach (var iqLevel in t.Result.GroupBy(x => x.Level))
                    {

                    }
                }, ctx);

            }
            catch (Exception ex)
            {
                tcs.TrySetException(ex);
            }


            return tcs.Task;
        }
    }
}