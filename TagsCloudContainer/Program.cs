using System.IO;
using Autofac;


namespace TagsCloudContainer
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = Path.GetDirectoryName(
                Path.GetDirectoryName(Directory.GetCurrentDirectory()));
            var scope = Configurator.GetContainer().BeginLifetimeScope();
            var creator = scope.Resolve<TagsCloudCreator>();
            creator.SetFontRandomColor();
            creator.TrySetImageFormat("png");
            creator.TrySetFontFamily("Comic Sans MS");
            creator.TrySetImageSize(500);
            creator.Create(Path.Combine(path, "input.txt"), path, "Cloud");
            creator.AddStopWord("aba");
            creator.Create(Path.Combine(path, "input.txt"), path, "Cloud2");
        }
    }
}
