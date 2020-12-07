using System.IO;

namespace TagsCloud.ClientGUI.Infrastructure
{
    public class PathSettings
    {
        public string PathToText { get; set; } =
            Path.Join(Directory.GetCurrentDirectory(), "Texts", "SourceText2.txt");

        public string PathToBoringWords { get; set; } =
            Path.Join(Directory.GetCurrentDirectory(), "Texts", "BoringWords.txt");

        public string PathToDictionary { get; set; } =
            Path.Join(Directory.GetCurrentDirectory(), "Texts", "ru_RU.dic");

        public string PathToAffix { get; set; }
            = Path.Join(Directory.GetCurrentDirectory(), "Texts", "ru_RU.aff");
    }
}