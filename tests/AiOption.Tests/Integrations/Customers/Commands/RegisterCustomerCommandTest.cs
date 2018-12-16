using System;
using AiOption.Application.ApplicationServices;
using AiOption.Domain.Customers.Commands;
using AiOption.TestCore;
using EventFlow.Exceptions;
using FluentAssertions;
using NUnit.Framework;

namespace AiOption.Tests.Integrations.Customers.Commands
{
    [Category(Category.Integrations)]
    public class RegisterCustomerCommandTest : IntegrationTest
    {
        [SetUp]
        public void SetupTerminateCustomerCommandTest()
        {
            EventFlowOptions
                .RegisterServices(r =>
                    r.Register(typeof(ICustomerProcessManagerService), typeof(CustomerProcessManagerService)));
        }

        [Test]
        public void CustomerRegisterCommand_WithMultipleRequest_ExceptionMustThrew()
        {
            // act
            Action action = () =>
            {
                PublishAsync(new CustomerRegisterCommand("m@email.com", "passcode", "invite")).Wait();
                PublishAsync(new CustomerRegisterCommand("m@email.com", "passcode", "invite")).Wait();
            };

            action.Should().Throw<DomainError>();
        }
    }
}