using EventFlow.Commands;
using iqoption.domain.Users;

namespace iqoption.domain.IqOption.Command {
    public class CreateOrUpdateIqAccountCommand : Command<IqAggregate, IqIdentity, IqAccount> {

        public IqAccount AccountDetail { get; }
        public string UserName { get; }

        public CreateOrUpdateIqAccountCommand(IqIdentity id, IqAccount accountDetail, string userName) : base(id) {
            UserName = userName;
            AccountDetail = accountDetail;
        }

    }
}