

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using AutoMapper;

namespace iqoption.core {

    public static class AutoMapperGlobalConfigurationExtensions {


        public static IEnumerable<Assembly> AsEnumerable() {

            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            foreach (var asm in
                Directory.GetFiles(baseDirectory)
                    .Where(x => Path.GetExtension(x).Equals(".dll", StringComparison.OrdinalIgnoreCase))
                    .Select(Assembly.Load)) {
                yield return asm;
            }
        }

        public static IMapper RegisterMapper() {
            var profiles = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(a => typeof(Profile).IsAssignableFrom(a));

            // Initialize AutoMapper with each instance of the profiles found.
            var mapperConfiguration = new MapperConfiguration(a => profiles.ToList().ForEach(a.AddProfile));

            return mapperConfiguration.CreateMapper();
        }
    }
    
}