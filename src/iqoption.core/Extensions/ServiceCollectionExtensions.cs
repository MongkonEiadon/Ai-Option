using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using At = AutoMapper;

namespace iqoption.core.Extensions
{
   public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddAutoMapper(this IServiceCollection This)
        {

            At.ServiceCollectionExtensions.AddAutoMapper(This);
            return This;
        }


      
    }
}
