using System.Drawing;
using Autofac;
using TagsCloudContainer.TagsCloudContainer;
using TagsCloudContainer.TagsCloudContainer.Interfaces;
using TagsCloudContainer.TagsCloudVisualization;
using TagsCloudContainer.TagsCloudVisualization.Interfaces;

namespace TagsCloudUI
{
    public class TagsCloudUiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var ulamSpiral = new UlamSpiral(new Point(250, 250));
            var archimedeanSpiral = new ArchimedeanSpiral(new Point(250, 250), 0.2, 1.0);
            var formConfig = new FormConfig(Color.Tan, new Size(600, 600), SpiralType.Archimedean);

            builder.RegisterInstance(formConfig);
            builder.RegisterInstance(ulamSpiral).As<ISpiral>();
            builder.RegisterInstance(archimedeanSpiral).As<ISpiral>().PreserveExistingDefaults();
            builder.RegisterType<LayouterFactory>().As<ILayouterFactory>();
            builder.RegisterType<WordValidator>().As<IWordValidator>();
            builder.RegisterType<BitmapSaver>().As<IBitmapSaver>();
            builder.RegisterType<TextParser>().As<ITextParser>();
            builder.RegisterType<CloudLayouter>().As<ILayouter>();
            builder.RegisterType<TagsVisualizer>();
            builder.RegisterType<TagsCloudContainer.TagsCloudContainer.TagsCloudContainer>().As<ITagsContainer>();
            builder.RegisterType<TagsCloudForm>();
        }
    }
}