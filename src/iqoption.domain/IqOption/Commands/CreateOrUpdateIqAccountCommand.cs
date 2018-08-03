using EventFlow.Commands;
using iqoption.domain.Users;

namespace iqoption.domain.IqOption.Command {
    public class CreateOrUpdateIqAccountCommand : Command<IqOptionAggregate, IqOptionIdentity, IqAccount> {

        public IqAccount AccountDetail { get; }
        public string UserName { get; }

        public CreateOrUpdateIqAccountCommand(IqOptionIdentity id, IqAccount accountDetail, string userName) : base(id) {
            UserName = userName;
            AccountDetail = accountDetail;
        }

    }
}