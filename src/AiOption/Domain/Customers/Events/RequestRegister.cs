﻿using AiOption.Domain.Accounts;
using AiOption.Domain.Common;
using EventFlow.Aggregates;

namespace AiOption.Domain.Customers.Events
{
    public class RequestRegister : AggregateEvent<CustomerAggregate, CustomerId>
    {
        public User UserName { get; }
        public Password Password { get; }
        public string InvitationCode { get; }

        public RequestRegister(
            User userName, 
            Password password, 
            string invitationCode)
        {
            UserName = userName;
            Password = password;
            InvitationCode = invitationCode;
        }
    }
}