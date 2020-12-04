using System.Drawing;
using System.IO;
using Autofac;
using Autofac.Core;
using NUnit.Framework;
using TagsCloud;
using TagsCloud.BoringWordsDetectors;
using TagsCloud.CloudRenderers;
using TagsCloud.ColorSelectors;
using TagsCloud.PointsLayouts;
using TagsCloud.StatisticProviders;
using TagsCloud.WordLayouters;
using TagsCloud.WordReaders;
using TagsCloud.WordSelectors;

namespace TagsCloudTests
{
    [TestFixture]
    public class TagsCloudTests
    {
        [Test]
        public void CreateCloud_FromTxt()
        {
            var directoryInfo = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent;
            if(directoryInfo == null) throw new DirectoryNotFoundException();
            if (!new DirectoryInfo($"{directoryInfo.FullName}\\Samples").Exists)
                directoryInfo.CreateSubdirectory("Samples");
            var samplePath = $"{directoryInfo.FullName}\\Samples\\sample.png";
            new FileInfo(samplePath).Delete();
            
            var builder = new ContainerBuilder();

            builder.RegisterInstance($"{directoryInfo.FullName}\\example.txt");
            builder.RegisterType<AllWordSelector>().As<IWordSelector>();
            builder.RegisterType<RegexWordReader>().As<IWordReader>();

            builder.RegisterType<ByCollectionBoringWordsDetector>().As<IBoringWordsDetector>();
            builder.RegisterType<StatisticProvider>().As<IStatisticProvider>();

            builder.RegisterInstance(new FontFamily("Arial"));
            builder.RegisterType<SpiralPoints>().As<IPointsLayout>();
            builder.RegisterType<WordLayouter>().SingleInstance().As<IWordLayouter>();

            builder.RegisterInstance(new[]
                {Color.Black, Color.Red, Color.Blue, Color.Green, Color.Yellow});
            builder.RegisterType<RandomColorSelector>().SingleInstance().As<IColorSelector>();
            
            builder.RegisterType<CloudRenderer>()
                .As<ICloudRenderer>()
                .WithParameters(new Parameter[]
                {
                    new NamedParameter("width", 3000),
                    new NamedParameter("height", 3000), 
                });
            
            Program.MakeCloud(builder.Build());
            var actual = new FileInfo(samplePath);
            Assert.True(actual.Exists);
        }
    }
}