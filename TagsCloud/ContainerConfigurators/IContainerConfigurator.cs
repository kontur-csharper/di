using Autofac;

namespace TagsCloud.ContainerConfigurators
{
    public interface IContainerConfigurator
    {
        IContainer Configure();
    }
}