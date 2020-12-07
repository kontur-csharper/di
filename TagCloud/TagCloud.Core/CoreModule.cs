using Autofac;
using TagCloud.Core.Layouting;
using TagCloud.Core.Layouting.Lazy;

namespace TagCloud.Core
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAdapter<ILazyLayouterFactory, ILayouter>(lazy => new LazyLayouterAdapter(lazy));

            builder.RegisterAssemblyTypes(ThisAssembly)
                .Except<LazyLayouterAdapter>()
                .AsImplementedInterfaces()
                .SingleInstance()
                .OwnedByLifetimeScope();
        }
    }
}