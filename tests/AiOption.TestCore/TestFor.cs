using System;
using AutoFixture;

namespace AiOption.TestCore
{
    public abstract class TestFor<TSut> : Test
    {
        private Lazy<TSut> _lazySut;
        protected TSut Sut => _lazySut.Value;
        
        public void SetUpTestsFor()
        {
            _lazySut = new Lazy<TSut>(CreateSut);
        }

        protected virtual TSut CreateSut()
        {
            return Fixture.Create<TSut>();
        }
    }
}