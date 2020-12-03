using Autofac;
using TagCloudUI.Infrastructure;

namespace TagCloudUI
{
    public class TagCloudUiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly).Except<AppSettings>()
                .AsImplementedInterfaces();

            builder.Register(context => context.Resolve<ISpiralFactory>().Create())
                .AsImplementedInterfaces().SingleInstance();
        }
    }
}