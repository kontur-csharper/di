using System;
using System.Linq;
using TagsCloud.Factory;
using TagsCloud.TextProcessing.WordsConfig;

namespace TagsCloud.TextProcessing.TextReaders
{
    public class ReadersFactory : ServiceFactory<IWordsReader>
    {
        private readonly WordConfig wordsConfig;

        public ReadersFactory(WordConfig wordsConfig)
        {
            this.wordsConfig = wordsConfig;
        }

        public override IWordsReader Create()
        {
            var reader = services.FirstOrDefault(pair => pair.Value().CanRead(wordsConfig.Path)).Value();
            if (reader == null)
                throw new InvalidOperationException("This file type is not supported");
            return reader;
        }
    }
}
