using System;
using System.Drawing;
using System.Linq;
using TagCloud.Interfaces;

namespace TagCloud
{
    public class TagCloudCreator : ITagCloudCreator
    {
        private readonly IWordsForCloudGenerator wordsForCloudGenerator;
        private readonly IWordsReader wordsReader;
        private readonly IWordsNormalizer wordsNormalizer;
        private readonly ICloudDrawer cloudDrawer;
        private readonly string inputFile;
        private readonly string boringWordsFile;

        public TagCloudCreator(IWordsForCloudGenerator wordsForCloudGenerator,
            IWordsReader wordsReader,
            IWordsNormalizer wordsNormalizer,
            ICloudDrawer cloudDrawer,
            string inputFile,
            string boringWordsFile)
        {
            this.wordsNormalizer = wordsNormalizer;
            this.cloudDrawer = cloudDrawer;
            this.wordsReader = wordsReader;
            this.wordsForCloudGenerator = wordsForCloudGenerator;
            this.inputFile = inputFile;
            this.boringWordsFile = boringWordsFile;
        }

        public Bitmap GetCloud()
        {
            var words = wordsReader.Get(inputFile);
            var normalizedWords = wordsNormalizer.NormalizeWords(words, wordsReader.Get(boringWordsFile).Select(x=>x.ToLower()).ToHashSet());
            var wordsForCloud = wordsForCloudGenerator.Generate(normalizedWords);
            return cloudDrawer.DrawCloud(wordsForCloud);
        }
    }
}