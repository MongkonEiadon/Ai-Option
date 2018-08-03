using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EventFlow.Commands;
using iqoption.core.data;
using iqoption.data.IqOptionAccount;
using iqoption.data.Services;
using iqoption.data.User;
using iqoption.domain.IqOption;
using iqoption.domain.IqOption.Command;
using Microsoft.AspNetCore.Identity;

namespace iqoption.data.Commands {
    public class CreateOrUpdateIqAccountCommandHandler : ICommandHandler<IqOptionAggregate, IqOptionIdentity, IqAccount,
        CreateOrUpdateIqAccountCommand> {
        private readonly IRepository<IqOptionAccountDto> _iqoptionAccountRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<UserDto> _userRepository;
        private readonly IUserService _userService;


        public CreateOrUpdateIqAccountCommandHandler(
            IUserService userService,
            IRepository<IqOptionAccountDto> iqoptionAccountRepository,
            UserManager<UserDto> userRepository,
            IMapper mapper) {
            _userService = userService;
            _iqoptionAccountRepository = iqoptionAccountRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IqAccount> ExecuteCommandAsync(IqOptionAggregate aggregate,
            CreateOrUpdateIqAccountCommand command,
            CancellationToken cancellationToken) {
            var dto = await _iqoptionAccountRepository.FirstOrDefaultAsync(x =>
                x.IqOptionUserId == command.AccountDetail.IqOptionUserId);

            if (dto != null) {
                dto.SsidUpdated = DateTime.Now;
                dto.Ssid = command.AccountDetail.Ssid;
                dto.UpdatedOn = DateTime.Now;

                await _iqoptionAccountRepository.UpdateAsync(dto);
            }

            else {
                dto = _mapper.Map<IqOptionAccountDto>(command.AccountDetail);
                dto.CreatedOn = DateTime.Now;
                dto.SsidUpdated = DateTime.Now;

                dto.User = await _userService.GetUserByNameAsync(command.UserName);

                await _iqoptionAccountRepository.InsertAndGetIdAsync(dto);
            }

            var result = _mapper.Map<IqOptionAccountDto, IqAccount>(dto);
            result.IsSuccess = true;
            return result;
        }
    }
}