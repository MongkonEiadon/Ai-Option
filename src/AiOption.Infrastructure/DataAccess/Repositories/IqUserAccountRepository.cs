using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AiOption.Application.Repositories;
using AiOption.Application.Repositories.ReadOnly;
using AiOption.Application.Repositories.WriteOnly;
using AiOption.Domain.Accounts;
using AiOption.Infrastructure.DataAccess.Extensions;
using AutoMapper;

using Dapper;

namespace AiOption.Infrastructure.DataAccess.Repositories
{
    public class IqUserAccountRepository : IIqOptionAccountReadOnlyRepository, IIqOptionWriteOnlyRepository {

        private readonly IDbConnection _connection;
        private readonly IMapper _mapper;

        public IqUserAccountRepository(IDbConnection connection, IMapper mapper) {
            _connection = connection;
            _mapper = mapper;
        }


        #region WriteAccess

        public async Task<bool> UpdateSecuredToken(int userId, string securedToken) {

            var sql = $"UPDATE iQOptionAccount SET ssid = @ssid, ssidUpdated = Getdate() WHERE IqUserId = @userId ";
            var dynamicParams = new DynamicParameters()
                .AddParameters("@ssid", securedToken)
                .AddParameters("@userId", userId);

            var rows = await _connection.ExecuteAsync(sql, dynamicParams);

            return rows == 1;
        }

        public async Task<bool> UpdateIsActiveAsync(int userId, bool isActive) {

            var sql = $"UPDATE iQOptionAccount SET isActive = @isActive, updatedDate = Getdate() WHERE IqUserId = @userId ";
            var dynamicParams = new DynamicParameters()
                .AddParameters("@isActive", isActive)
                .AddParameters("@userId", userId);

            var rows = await _connection.ExecuteAsync(sql, dynamicParams);

            return rows == 1;
        }

        #endregion

        #region ReadAccess

        public async Task<IEnumerable<Account>> GetAllTask() {

            var sql = "SELECT * FROM Account";
            var result = await _connection.QueryAsync<IqUserAccountDto>(sql);

            return _mapper.Map<IEnumerable<Account>>(result);
        }

        public async Task<Account> GetByUserIdTask(int userId)
        {
            var sql = "SELECT * FROM Account WHERE Id = @userId";
            var dynamicParams = new DynamicParameters();
            dynamicParams.Add("@userId", userId);
            var result = await _connection.QueryAsync<IqUserAccountDto>(sql, dynamicParams);

            var iqAccountDtos = result as IqUserAccountDto[] ?? result.ToArray();
            if (iqAccountDtos.Any()) {
                return _mapper.Map<IqUserAccountDto, Account>(iqAccountDtos.FirstOrDefault());
            }

            return null;
        }

        public async Task<Account> GetByUserNameTask(string userName)
        {
            var sql = "SELECT * FROM Account WHERE IqOptionUserName = @IqUserName";
            var dynamicParams = new DynamicParameters();
            dynamicParams.Add("@IqUserName", userName);
            var result = await _connection.QueryAsync<IqUserAccountDto>(sql, dynamicParams);

            var iqAccountDtos = result as IqUserAccountDto[] ?? result.ToArray();
            if (iqAccountDtos.Any())
            {
                return _mapper.Map<IqUserAccountDto, Account>(iqAccountDtos.FirstOrDefault());
            }

            return null;
        }


        public async Task<IEnumerable<Account>> GetActiveAccountForOpenTradingsAsync() {
            var sql = $"SELECT * FROM IqOptionAccounts Where IsActive = @IsActive";
            var param = new DynamicParameters();
            param.Add("@IsActive", true);

            var result = await _connection.QueryAsync<IqUserAccountDto>(sql, param);

            if (result != null && result.Any()) {
                return _mapper.Map<IEnumerable<Account>>(result);
            }

            return Enumerable.Empty<Account>();
        }



        #endregion


    }
}
