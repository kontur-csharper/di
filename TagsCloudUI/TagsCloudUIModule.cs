﻿using System.Drawing;
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
            var spiral = new ArchimedeanSpiral(new Point(250, 250), 0.2, 1.0);
            var formConfig = new FormConfig(Color.Tan, Brushes.Black, "Times New Roman", new Size(600, 600));

            builder.RegisterInstance(spiral).As<ISpiral>();
            builder.RegisterInstance(formConfig);
            builder.RegisterType<WordValidator>().As<IWordValidator>();
            builder.RegisterType<BitmapSaver>().As<IBitmapSaver>();
            builder.RegisterType<TextParser>().As<ITextParser>();
            builder.RegisterType<CircularCloudLayouter>().As<ILayouter>();
            builder.RegisterType<TagsCloudContainer.TagsCloudContainer.TagsCloudContainer>().As<ITagsContainer>();
            builder.RegisterType<TagsCloudForm>();
        }
    }
}