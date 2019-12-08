using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using Autofac;
using TagCloudContainer.Api;
using TagCloudContainer.Implementations;

namespace TagCloudContainer.fluent
{
    public class TagCloudConfig
    {
        public readonly string inputFile;
        private IWordProvider wordProvider;
        private Type wordProcessor;
        private Type wordVisualizer;
        private Type cloudLayouter;
        private Type wordCloudLayouter;
        private Type sizeProvider;
        private Type brushProvider;
        private Type penProvider;
        private ImageFormat imageFormat;
        private DrawingOptions options;

        private TagCloudConfig(TagCloudConfig parent)
        {
            inputFile = parent.inputFile;
            wordProvider = parent.wordProvider;
            wordProcessor = parent.wordProcessor;
            wordVisualizer = parent.wordVisualizer;
            cloudLayouter = parent.cloudLayouter;
            wordCloudLayouter = parent.wordCloudLayouter;
            sizeProvider = parent.sizeProvider;
            brushProvider = parent.brushProvider;
            imageFormat = parent.imageFormat;
            options = parent.options;
        }

        public TagCloudConfig(string inputFile)
        {
            this.inputFile = inputFile;
            imageFormat = ImageFormat.Png;
            options = new DrawingOptions();
            wordProvider = new TxtFileReader(inputFile);
            wordProcessor = typeof(LowercaseWordProcessor);
            cloudLayouter = typeof(CircularCloudLayouter);
            sizeProvider = typeof(StringSizeProvider);
            brushProvider = typeof(OneColorBrushProvider);
            penProvider = typeof(OneColorPenProvider);
            wordCloudLayouter = typeof(WordCloudLayouter);
            wordVisualizer = typeof(TagCloudVisualizer);
        }

        public TagCloudConfig UsingWordProvider(IWordProvider wordProvider)
        {
            return new TagCloudConfig(this) {wordProvider = wordProvider};
        }

        public TagCloudConfig UsingWordProcessor(Type wordProcessor)
        {
            return new TagCloudConfig(this) {wordProcessor = wordProcessor};
        }

        public TagCloudConfig UsingWordVisualizer(Type wordVisualizer)
        {
            return new TagCloudConfig(this) {wordVisualizer = wordVisualizer};
        }

        public TagCloudConfig UsingCloudLayouter(Type cloudLayouter)
        {
            return new TagCloudConfig(this) {cloudLayouter = cloudLayouter};
        }

        public TagCloudConfig UsingWordCloudLayouter(Type wordCloudLayouter)
        {
            return new TagCloudConfig(this) {wordCloudLayouter = wordCloudLayouter};
        }

        public TagCloudConfig UsingStringSizeProvider(Type sizeProvider)
        {
            return new TagCloudConfig(this) {sizeProvider = sizeProvider};
        }

        public TagCloudConfig UsingWordBrushProvider(Type brushProvider)
        {
            return new TagCloudConfig(this) {brushProvider = brushProvider};
        }

        public TagCloudConfig UsingRectanglePenProvider(Type penProvider)
        {
            return new TagCloudConfig(this) {penProvider = penProvider};
        }

        public TagCloudConfig Using(ImageFormat imageFormat)
        {
            return new TagCloudConfig(this) {imageFormat = imageFormat};
        }

        public TagCloudConfig UsingBackgroundBrush(Brush brush)
        {
            return new TagCloudConfig(this) {options = options.WithBackgroundBrush(brush)};
        }

        public TagCloudConfig UsingFont(Font font)
        {
            return new TagCloudConfig(this) {options = options.WithFont(font)};
        }

        public TagCloudConfig UsingWordBrush(Brush brush)
        {
            return new TagCloudConfig(this) {options = options.WithWordBrush(brush)};
        }

        public TagCloudConfig UsingPen(Pen pen)
        {
            return new TagCloudConfig(this) {options = options.WithPen(pen)};
        }

        public void SaveToFile(string file)
        {
            var container = PrepareContainer();
            var bitmap = container.Resolve<Image>();
            bitmap.Save(file, imageFormat);
        }

        private IContainer PrepareContainer()
        {
            var builder = new ContainerBuilder();
            builder.Register(c => wordProvider).As<IWordProvider>().SingleInstance();
            builder.RegisterType(wordProcessor).As<IWordProcessor>().SingleInstance();

            builder.Register(c =>
            {
                var words = c.Resolve<IWordProvider>().GetWords();
                return c.Resolve<IWordProcessor>().Process(words);
            }).As<IEnumerable<string>>();

            builder.RegisterType(cloudLayouter).As<ICloudLayouter>().SingleInstance();
            builder.RegisterType(wordCloudLayouter).As<IWordCloudLayouter>().SingleInstance();
            builder.RegisterType(sizeProvider).As<IStringSizeProvider>().SingleInstance();
            builder.RegisterType(brushProvider).As<IWordBrushProvider>().SingleInstance();
            builder.RegisterType(penProvider).As<IRectanglePenProvider>().SingleInstance();

            builder.Register(c => options).As<DrawingOptions>().SingleInstance();
            builder.Register(c => c.Resolve<DrawingOptions>().Font).As<Font>().SingleInstance();

            builder.RegisterType(wordVisualizer).As<IWordVisualizer>();

            builder.Register(c => c.Resolve<IWordVisualizer>().CreateImageWithWords(
                c.Resolve<IEnumerable<string>>(),
                c.Resolve<DrawingOptions>())
            ).As<Image>();
            return builder.Build();
        }
    }
}