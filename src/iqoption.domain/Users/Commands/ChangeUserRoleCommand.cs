using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EventFlow;
using EventFlow.Commands;
using EventFlow.Core;
using iqoption.domain.Users.Commands.Results;

namespace iqoption.domain.Users.Commands
{
    public class ChangeUserRoleCommand : Command<UserAggregrate, UserIdentity, ChangeUserRoleCommandResult> {
        public string UserId { get; }
        public string RoleName { get; }

        public ChangeUserRoleCommand(UserIdentity aggregateId, string userId, string roleName) : base(aggregateId) {
            UserId = userId;
            RoleName = roleName;
        }

    }
}

    