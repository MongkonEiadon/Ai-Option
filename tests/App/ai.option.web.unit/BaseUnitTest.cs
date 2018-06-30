using System;
using System.Linq;
using System.Reflection;
using ai.option.web.AutoMapper;
using AutofacContrib.NSubstitute;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Map = AutoMapper.Mapper;

namespace ai.option.web.unit {
    public class BaseUnitTest : IDisposable {
        public AutoSubstitute AutoSubstitute { get; private set; }
        public IServiceCollection ServiceCollection { get; set; }
        public IServiceProvider ServiceProvider { get; private set; }

 
        public BaseUnitTest(){

            var mapper = new MapperConfiguration(c => {
                var all = Assembly.GetExecutingAssembly().GetReferencedAssemblies().Select(Assembly.Load)
                    .SelectMany(x => x.DefinedTypes)
                    .Where(t => typeof(Profile).IsAssignableFrom(t) && !t.IsAbstract && t.IsPublic);


                foreach (var tinfo in all) c.AddProfile(tinfo.AsType());
                
            });

            AutoSubstitute = new AutoSubstitute();
            AutoSubstitute.Provide<IMapper>(mapper.CreateMapper());

            
        }
        

        public void Dispose() {
            AutoSubstitute?.Dispose();
            Map.Reset();
        }
    }
}