using System;
using System.Linq;
using System.Reflection;
using AutofacContrib.NSubstitute;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace ai.option.web.unit {
    [TestFixture]
    public abstract class BaseUnitTest {
        public AutoSubstitute AutoSubstitute { get; private set; }
        public IServiceCollection ServiceCollection { get; set; }
        public IServiceProvider ServiceProvider { get; private set; }

        [OneTimeSetUp]
        public void OneTimeSetup() {
            Mapper.Reset();
            Mapper.Initialize(c => {
                var all = Assembly.GetExecutingAssembly().GetReferencedAssemblies().Select(Assembly.Load)
                    .SelectMany(x => x.DefinedTypes)
                    .Where(t => typeof(Profile).IsAssignableFrom(t) && !t.IsAbstract && t.IsPublic);


                foreach (var tinfo in all) c.AddProfile(tinfo.AsType());
            });

            AutoSubstitute = new AutoSubstitute();
            var imapper = AutoSubstitute.Provide(Mapper.Instance);
        }
    }
}