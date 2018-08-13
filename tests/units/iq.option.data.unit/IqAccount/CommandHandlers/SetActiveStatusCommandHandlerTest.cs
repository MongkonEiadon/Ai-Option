using System.Threading;
using System.Threading.Tasks;
using Autofac.Extras.Moq;
using iqoption.data.IqOptionAccount.CommandHandlers;
using iqoption.data.Services;
using iqoption.domain.IqOption;
using iqoption.domain.IqOption.Commands;
using Moq;
using Xunit;

namespace iq.option.data.unit.IqAccount.CommandHandlers {

    public class SetActiveStatusCommandHandlerTest {

        public SetActiveStatusCommandHandlerTest() {

        }


        [Fact]
        public async Task SetActiveStatusCommand_WithPassParameterThroughCommand_SqlWrapperShouldReceivedCommand() {

            using (var moq = AutoMock.GetLoose()) {

                // arrange
                var mocker = moq.Mock<ISqlWrapper>();

                // act
                var result = await moq.Create<IqAccountCommandHandlers>()
                    .ExecuteCommandAsync(default(IqAggregate),
                        new SetActiveAccountcommand(IqIdentity.New, new ActiveAccountItem(true, 1)), default(CancellationToken));

                // assert
                mocker.Verify(sql => sql.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>()), Times.Once);
            }
        }

    }

}