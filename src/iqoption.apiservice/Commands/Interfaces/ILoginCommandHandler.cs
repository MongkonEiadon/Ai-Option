using iqoption.core;
using iqoption.domain.IqOption.Command;

namespace iqoption.apiservice {
    public interface ILoginCommandHandler : IRequestHandler<LoginCommand, LoginCommandResult> {
    }
}