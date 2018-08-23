using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EventFlow.Queries;
using iqoption.data.Services;
using iqoption.domain.IqOption;
using iqoption.domain.IqOption.Queries;

namespace iqoption.data.IqOptionAccount.Queries {
    public class GetIqAccountByIqUserIdQueryHandler : IQueryHandler<GetIqAccountByIqUserIdQuery, IqAccount>
    {
        private readonly IMapper _mapper;
        private readonly ISqlWrapper _sqlWrapper;

        public GetIqAccountByIqUserIdQueryHandler(IMapper mapper, ISqlWrapper sqlWrapper) {
            _mapper = mapper;
            _sqlWrapper = sqlWrapper;
        }


        public async Task<IqAccount> ExecuteQueryAsync(GetIqAccountByIqUserIdQuery query, CancellationToken cancellationToken) {

            var result = await _sqlWrapper
                .QueryAsync<IqOptionAccountDto>($"select * from IqOptionAccount where {nameof(IqOptionAccountDto.IqOptionUserId)} = @userId",
                    new {userId = query.UserId});

            var resultList = result as IqOptionAccountDto[] ?? result.ToArray();


            if (resultList.ToList().Any()) {
                return _mapper.Map<IqAccount>(resultList.FirstOrDefault());
            }

            return null;
        }
    }
}