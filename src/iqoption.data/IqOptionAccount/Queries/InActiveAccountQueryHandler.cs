using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EventFlow.Queries;
using iqoption.core.data;
using iqoption.domain.IqOption;
using iqoption.domain.IqOption.Queries;

namespace iqoption.data.IqOptionAccount.Queries {
    public class InActiveAccountQueryHandler :
        IQueryHandler<InActiveAccountQuery, List<IqAccount>> {
        private readonly IRepository<IqOptionAccountDto> _iqAccountRepository;
        private readonly IMapper _mapper;

        public InActiveAccountQueryHandler(IRepository<IqOptionAccountDto> iqAccountRepository, IMapper mapper) {
            _iqAccountRepository = iqAccountRepository;
            _mapper = mapper;
        }

        public Task<List<IqAccount>>
            ExecuteQueryAsync(InActiveAccountQuery query, CancellationToken cancellationToken) {
            return _iqAccountRepository
                .GetAllListAsync(x => !x.IsActive)
                .ContinueWith(t => _mapper.Map<List<IqAccount>>(t.Result), cancellationToken);
        }
    }
}