using System.Reflection;

using AiOption.Application.Repositories;
using AiOption.Infrastructure.DataAccess;
using AiOption.Infrastructure.DataAccess.Repositories;

using Autofac;

using EventFlow.MsSql;
using EventFlow.MsSql.Extensions;

using Microsoft.EntityFrameworkCore;

using Module = Autofac.Module;

namespace AiOption.Infrastructure.Modules {

    public class InfrastructureModule : Module {

        protected override void Load(ContainerBuilder builder) {

            builder.RegisterAssemblyTypes(typeof(InfrastructureModule).Assembly)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterType<AiOptionDbContext>()
                .As<DbContext>()
                .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(EfCoreRepositoryBase<,>)).As(typeof(IWriteOnlyRepository<,>))
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(x => x.FullName.EndsWith("Persistence"))
                .AsSelf()
                .SingleInstance();
        }

    }

}