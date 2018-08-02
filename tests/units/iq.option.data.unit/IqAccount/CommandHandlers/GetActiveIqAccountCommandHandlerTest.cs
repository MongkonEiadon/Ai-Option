using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutofacContrib.NSubstitute;
using Dapper;
using iqoption.data.IqOptionAccount.CommandHandlers;
using iqoption.domain.IqOption;
using iqoption.domain.IqOption.Commands;
using Moq;
using NSubstitute;
using Shouldly;
using Xunit;
using AutoMock = Autofac.Extras.Moq.AutoMock;

namespace iq.option.data.unit.IqAccount.CommandHandlers
{
    public class GetActiveIqAccountCommandHandlerTest
    {
        private IDbConnection _connection;
        private AutoSubstitute AutoSubstitute { get; }

        public GetActiveIqAccountCommandHandlerTest()
        {
            AutoSubstitute = new AutoSubstitute();
            _connection = AutoSubstitute.Resolve<IDbConnection>();

            AutoSubstitute.Provide<Func<IDbConnection>>(() => _connection);
        }


        [Fact]
        public async Task Test()
        {
            // arrange
            //_connection.ExecuteAsync(Arg.Any<string>()).Returns(c => Task.FromResult(1));


            // act
            var result = await AutoSubstitute.Resolve<IqAccountCommandHandlers>()
                .ExecuteCommandAsync(
                    default(IqOptionAggregate), 
                    new StoreSsidCommand(IqOptionIdentity.New, "", ""), 
                    default(CancellationToken));

            // assert
            result.IsSuccess.ShouldBe(true);

        }
    }
}
