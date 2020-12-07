using Autofac;

namespace MyStem.Wrapper
{
    public class WrapperModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly)
                .AsImplementedInterfaces()
                .OwnedByLifetimeScope()
                .InstancePerLifetimeScope();
        }
    }
}