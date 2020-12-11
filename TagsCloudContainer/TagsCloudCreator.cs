using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace TagsCloudContainer
{
    public class TagsCloudCreator
    {
        private FontFamily fontFamily;
        private readonly ICloudDrawer cloudDrawer;
        private readonly IFontSizeCalculator fontSizeCalculator;
        private readonly Dictionary<string, IFileReader> fileReaders;
        private readonly IImageSaver[] availableImages;
        private readonly List<IWordsFilter> wordsFilters;
        private StopWords stopWords;

        public TagsCloudCreator(
            ICloudDrawer cloudDrawer, 
            IFontSizeCalculator fontSizeCalculator, 
            IFileReader[] fileReaders, 
            IImageSaver[] availableImages, 
            IWordsFilter[] wordsFilters,
            StopWordsFilter stopWordsFilter)
        {
            this.cloudDrawer = cloudDrawer;
            this.fontSizeCalculator = fontSizeCalculator;
            this.fileReaders = InitializeFileReders(fileReaders);
            this.availableImages = availableImages;
            fontFamily = new FontFamily("Arial");
            stopWords = stopWordsFilter.StopWords;
            this.wordsFilters = new List<IWordsFilter> { stopWordsFilter };
            this.wordsFilters.AddRange(wordsFilters);
        }

        private Dictionary<string, IFileReader> InitializeFileReders(IFileReader[] fileReaders)
        {
            return fileReaders.ToDictionary(fr => fr.Format, fr => fr);
        }

        public void Create(string filePath, string targetPath, string imageName)
        {
            IFileReader reader;
            var fileExtension = Path.GetExtension(filePath).TrimStart('.');
            if (!fileReaders.TryGetValue(fileExtension, out reader))
                throw new ArgumentException();
            var words = reader.ReadAllLines(filePath); 
            words = FilterWords(words);
            var orderedWordsWithFonts = fontSizeCalculator.CalculateFontSize(words, fontFamily)
                .OrderByDescending(word => word.Font.Size).ToList();
            cloudDrawer.DrawCloud(orderedWordsWithFonts, targetPath, imageName);
        }

        public void AddStopWord(string stopWord)
        {
            stopWords.Add(stopWord);
        }

        public void RemoveStopWord(string stopWord)
        {
            stopWords.Remove(stopWord);
        }

        public bool TrySetFontFamily(string fontFamilyName)
        {
            try
            {
                fontFamily = new FontFamily(fontFamilyName);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool TrySetFontColor(string colorName)
        {
            var color = Color.FromName(colorName);
            if (color.R == 0 && color.G == 0 && color.B == 0 && color.Name != "Black")
                return false;
            cloudDrawer.ColorProvider = new FixedColorProvider(color);
            return true;
        }

        public void SetFontRandomColor()
        {
            cloudDrawer.ColorProvider = new RandomColorProvider();
        }

        public bool TrySetImageSize(int imageSize)
        {
            if (imageSize > 2000 || imageSize < 100)
                return false;
            cloudDrawer.ChangeImageSize(imageSize);
            return true;
        }

        public bool TrySetImageFormat(string imageFormat)
        {
            foreach (var imageSaver in availableImages)
            {
                if (imageSaver.FormatName == imageFormat)
                {
                    cloudDrawer.ImageSaver = imageSaver;
                    return true;
                }
            }

            return false;
        }

        public string GetImageFormat()
        {
            return cloudDrawer.ImageSaver.FormatName;
        }

        private List<string> FilterWords(IEnumerable<string> words)
        {
            foreach (var filter in wordsFilters)
            {
                words = filter.Filter(words);
            }

            return words.ToList();
        }
    }
}
