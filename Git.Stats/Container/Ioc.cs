using Autofac;
using Command.Infrastructure;
using Git.Stats.Infrastructure;
using Git.Stats.Infrastructure.Services.Implementations;
using Git.Stats.Infrastructure.Services.Interfaces;

namespace Git.Stats.Container
{
    public static class Ioc
    {
        private static readonly IContainer container;

        static Ioc()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<GitStatsController>().As<ICommandController>().SingleInstance();
            builder.RegisterType<CommandHandler>().SingleInstance();
            builder.RegisterType<GetStatsCommandHandler>().SingleInstance();
            builder.RegisterAssemblyTypes(typeof(IService).Assembly)
                .Where(x => typeof(IService).IsAssignableFrom(x))
                .AsImplementedInterfaces()
                .AsSelf()
                .SingleInstance();
            container = builder.Build();
        }

        public static T Resolve<T>()
        {
            return container.Resolve<T>();
        }
    }
}
