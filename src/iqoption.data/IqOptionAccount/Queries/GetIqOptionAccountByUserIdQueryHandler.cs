using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EventFlow.Queries;
using iqoption.core.data;
using iqoption.data.IqOptionAccount;
using iqoption.domain.IqOption;
using iqoption.domain.IqOption.Queries;

namespace iqoption.data.Queries
{
    public class GetIqOptionAccountByUserIdQueryHandler :
        IQueryHandler<GetIqOptionAccountByUserIdQuery, IEnumerable<IqAccount>> {
        private readonly IRepository<IqOptionAccountDto> _iqOptionAccountRepository;
        private readonly IMapper _mapper;

        public GetIqOptionAccountByUserIdQueryHandler(
            IRepository<IqOptionAccountDto> iqOptionAccountRepository,
            IMapper mapper) {
            _iqOptionAccountRepository = iqOptionAccountRepository;
            _mapper = mapper;
        }

        public Task<IEnumerable<IqAccount>> ExecuteQueryAsync(domain.IqOption.Queries.GetIqOptionAccountByUserIdQuery query, CancellationToken cancellationToken) {

            return _iqOptionAccountRepository
                .GetAllListAsync(t => t.User.Id == query.UserName.ToString())
                .ContinueWith(t => t.Result.Select(x => _mapper.Map<IqOptionAccountDto, IqAccount>(x)), cancellationToken);

        }
    }


}
