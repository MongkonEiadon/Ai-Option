using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EventFlow.Queries;
using iqoption.core.data;
using iqoption.data.Services.ReadModelServices;
using iqoption.domain.IqOption;
using iqoption.domain.IqOption.Queries;

namespace iqoption.data.IqOptionAccount.Queries {
    public class InActiveAccountQueryHandler :
        IQueryHandler<InActiveAccountQuery, IEnumerable<IqAccount>> {
        private readonly IRepository<IqOptionAccountDto> _iqAccountRepository;
        private readonly IMapper _mapper;
        private readonly IIqAccountQueryService _iqAccountQueryService;

        public InActiveAccountQueryHandler(IRepository<IqOptionAccountDto> iqAccountRepository, IMapper mapper, IIqAccountQueryService iqAccountQueryService) {
            _iqAccountRepository = iqAccountRepository;
            _mapper = mapper;
            _iqAccountQueryService = iqAccountQueryService;
        }

        public Task<IEnumerable<IqAccount>>
            ExecuteQueryAsync(InActiveAccountQuery query, CancellationToken cancellationToken) {
            return _iqAccountQueryService.GetInActiveIqAccountsAsync(cancellationToken);
        }
    }
}