using System.Collections.Generic;
using System.Linq;
using AiOption.Domain.Customers;
using AiOption.Domain.IqAccounts;
using AutoFixture;
using AutoFixture.AutoMoq;
using EventFlow;
using EventFlow.Queries;
using Moq;

namespace AiOption.TestCore
{
    public abstract class Test
    {
        public Test()
        {
            Fixture = new Fixture().Customize(new AutoMoqCustomization());

            Fixture.Customize<CustomerId>(x => x.FromFactory(() => CustomerId.New));
            Fixture.Customize<IqAccountId>(x => x.FromFactory(() => IqAccountId.New));

            InjectMock<IQueryProcessor>();
            InjectMock<ICommandBus>();
        }

        protected IFixture Fixture { get; }

        protected T A<T>()
        {
            return Fixture.Create<T>();
        }

        protected List<T> Many<T>(int count = 3)
        {
            return Fixture.CreateMany<T>(count).ToList();
        }

        protected T Mock<T>()
            where T : class
        {
            return new Mock<T>().Object;
        }

        protected T Inject<T>(T instance)
            where T : class
        {
            Fixture.Inject(instance);
            return instance;
        }


        protected Mock<T> InjectMock<T>(params object[] args)
            where T : class
        {
            var mock = new Mock<T>(args);
            Fixture.Inject(mock.Object);
            return mock;
        }
    }
}