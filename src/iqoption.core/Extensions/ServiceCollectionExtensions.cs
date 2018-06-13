using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using At = AutoMapper;

namespace iqoption.core.Extensions
{
   public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddAutoMapper(this IServiceCollection This)
        {

            At.ServiceCollectionExtensions.AddAutoMapper(This);

            var all = Assembly
                .GetEntryAssembly()
                .GetReferencedAssemblies()
                .Select(Assembly.Load)
                .SelectMany(x => x.DefinedTypes)
                .Where(t => typeof(Profile).GetTypeInfo().IsAssignableFrom(t.AsType()));

           foreach (var ti in all) {
               var t = ti.AsType();
               if (t == typeof(IProfileExpression)) {
                   Mapper.Initialize(c => c.AddProfiles(t));
               }
           }

            return This;
        }
    }
}
