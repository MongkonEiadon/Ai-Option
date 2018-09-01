using AiOption.Application.Repositories;
using AiOption.Infrastructure.DataAccess;
using AiOption.Infrastructure.DataAccess.Repositories;
using AiOption.Infrastructure.PersistanceServices;

using Autofac;

using Microsoft.EntityFrameworkCore;

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

            //builder.RegisterAssemblyTypes(ThisAssembly)
            //    .Where(x => x.FullName.EndsWith("PersistenceService"))
            //    .AsSelf()
            //    .AsImplementedInterfaces()
            //    .SingleInstance();

            builder.RegisterType<TraderPersistenceService>().AsSelf().As<ITraderPersistenceService>().SingleInstance();
            builder.RegisterType<FollowerPersistenceService>().AsSelf().As<IFollowerPersistenceService>()
                .SingleInstance();
        }

    }

}