using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutofacContrib.NSubstitute;
using AutoMapper;
using iqoption.core;
using iqoption.data.IqOptionAccount;
using iqoption.data.IqOptionAccount.Queries;
using iqoption.data.Migrations;
using iqoption.data.Services;
using iqoption.domain.IqOption.Queries;
using Moq;
using NSubstitute;
using Shouldly;
using Xunit;
using AutoMock = Autofac.Extras.Moq.AutoMock;

namespace iq.option.data.unit.IqAccount.Queries
{
    public class GetIqAccountByIqAccountUserIdTest : IDisposable {
        private AutoSubstitute AutoSubstitute { get; }

        public GetIqAccountByIqAccountUserIdTest() {

            Mapper.Initialize(c => { c.AddProfiles(typeof(iqoption.data.AiOptionContext).Assembly); });
            Mapper.AssertConfigurationIsValid();

            AutoSubstitute = new AutoSubstitute();
            AutoSubstitute.Provide<IMapper>(Mapper.Instance);
        }

        [Fact]
        public async Task ExecuteQueryAsync_WhenQueryWrapperRetrunEmpty_NullResponsed() {

            // arrange
            
            //act
            var query = await AutoSubstitute.Resolve<GetIqAccountByIqUserIdQueryHandler>()
                .ExecuteQueryAsync(new GetIqAccountByIqUserIdQuery(123), CancellationToken.None);

            //assert
            query.ShouldBeNull();
        }

        [Fact]
        public async Task ExecuteQueryAsync_WhenQueryWrapperReturnMorethanOne_IqAccountReturned() {

            // arrange
            var sqlWrapper = AutoSubstitute.Resolve<ISqlWrapper>();
            sqlWrapper.QueryAsync<IqOptionAccountDto>(Arg.Any<string>(), Arg.Any<object>())
                .Returns(info => new [] {
                    new IqOptionAccountDto(),
                    new IqOptionAccountDto()
                });

            //act
            var query = await AutoSubstitute.Resolve<GetIqAccountByIqUserIdQueryHandler>()
                .ExecuteQueryAsync(new GetIqAccountByIqUserIdQuery(123), CancellationToken.None);

            //assert
            query.ShouldNotBeNull();

        }


        public void Dispose() {
            AutoSubstitute?.Dispose();
            Mapper.Reset();
        }
    }
}
