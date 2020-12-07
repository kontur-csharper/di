using Autofac;

namespace TagCloud.Gui
{
    public class GuiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly)
                .AsImplementedInterfaces()
                .SingleInstance()
                .OwnedByLifetimeScope();
        }
    }
}