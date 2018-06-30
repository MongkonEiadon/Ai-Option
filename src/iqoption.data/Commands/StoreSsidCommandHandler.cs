using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using iqoption.core;
using iqoption.core.data;
using iqoption.data.Model;
using iqoption.domain.IqOption.Command;

namespace iqoption.data.Commands {
    public class StoreSsidCommandHandler  {
        private readonly IRepository<IqOptionAccountDto> _accountDto;

        public StoreSsidCommandHandler(IRepository<IqOptionAccountDto> accountDto) {
            _accountDto = accountDto;
        }

        public async Task<string> HandleRequestAsync(StoreSsidCommand request, CancellationToken ct = default(CancellationToken)) {

            var account = await _accountDto.FirstOrDefaultAsync(x => x.IqOptionUserName.ToUpper() == request.EmailAddress.ToUpper());

            if (account != null) {
                account.Ssid = request.Ssid;
                account.SsidUpdated = DateTime.Now;

                _accountDto.Update(account);
                return account.Ssid;
            }


            return "";
        }
    }


    public class StoreSsidCommandValidation : AbstractValidator<StoreSsidCommand> {
        public StoreSsidCommandValidation() {
            RuleFor(m => m.EmailAddress).EmailAddress();
            RuleFor(m => m.Ssid).NotNull();
        }
    }

}