using TagCloud.Core.ColoringAlgorithms;
using TagCloud.Core.LayoutAlgorithms;

namespace TagCloudUI.Infrastructure
{
    public interface IAppSettings
    {
        string InputPath { get; }
        string OutputPath { get; }
        int ImageWidth { get; }
        int ImageHeight { get; }
        LayoutAlgorithmType LayoutAlgorithmType { get; }
        ColoringTheme ColoringTheme { get; }
        string FontName { get; }
        string ImageFormat { get; }
        int WordsCount { get; }
    }
}