using AutofacContrib.NSubstitute;
using EventFlow.Aggregates;
using EventFlow.Core;

namespace AiOption.TestCore
{
    public abstract class Test
    {
        protected AutoSubstitute AutoSubstitute { get; }

        protected T Provide<T, TImplement>() => AutoSubstitute.Provide<T, TImplement>();
        protected T Resolve<T>() => AutoSubstitute.Resolve<T>();


        public Test()
        {
            AutoSubstitute = new AutoSubstitute();
        }

        protected T A<T>()
            where T: class
        {
            return AutoSubstitute.Resolve<T>();
        }
        
    }
}