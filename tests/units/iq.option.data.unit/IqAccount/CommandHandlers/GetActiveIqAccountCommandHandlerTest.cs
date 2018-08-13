using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutofacContrib.NSubstitute;
using Dapper;
using iqoption.data.IqOptionAccount.CommandHandlers;
using iqoption.data.Services;
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
        private AutoSubstitute AutoSubstitute { get; }

        public GetActiveIqAccountCommandHandlerTest()
        {
            AutoSubstitute = new AutoSubstitute();
        }


        [Fact]
        public async Task StoreSsidCommand_WithUpdate1Record_ResultIsSuccessWithTrueReturned() {

            using (var moq = AutoMock.GetLoose()) {

                // arrange
                var wrapper = moq.Mock<ISqlWrapper>();
                wrapper.Setup(c => c.ExecuteAsync(It.IsAny<string>())).Returns(Task.FromResult(1));

                // act
                var result = await moq.Create<IqAccountCommandHandlers>().ExecuteCommandAsync(
                    default(IqAggregate), new StoreSsidCommand(IqIdentity.New, "", ""),
                    default(CancellationToken));

                // assert
                result.IsSuccess.ShouldBe(true);

            }
        }


        [Fact]
        public async Task StoreSsidCommand_WithUpdateZeroRecord_ResultWithFalseIsSuccessReturned() {

            using (var moq = AutoMock.GetLoose()) {

                // arrange
                var mocker = moq.Mock<ISqlWrapper>();
                mocker.Setup(c => c.ExecuteAsync(It.IsAny<string>())).Returns(Task.FromResult(0));

                // act
                var result = await moq.Create<IqAccountCommandHandlers>()
                    .ExecuteCommandAsync(
                        default(IqAggregate), new StoreSsidCommand(IqIdentity.New, "", ""), default(CancellationToken));

                // assert
                result.IsSuccess.ShouldBe(false);
            }
        }


    }

}
