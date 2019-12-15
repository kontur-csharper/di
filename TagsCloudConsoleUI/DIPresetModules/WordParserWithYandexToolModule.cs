﻿using Autofac;
using SyntaxTextParser;
using SyntaxTextParser.Architecture;
using SyntaxTextParser.YandexParser;

namespace TagsCloudConsoleUI.DIPresetModules
{
    internal class WordParserWithYandexToolModule : Module
    {
        private readonly string[] boringSpeech;
        private readonly string[] boringWords;
        protected readonly string FilePath = "./YandexParser";
        protected readonly string FileName = "mystem.exe";

        public WordParserWithYandexToolModule(string[] boringSpeech, string[] boringWords)
        {
            this.boringSpeech = boringSpeech;
            this.boringWords = boringWords;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TextParser>();
            builder.RegisterType<ToLowerFormatter>().As<IElementFormatter>();

            builder.RegisterType<TxtFileReader>().As<IFileReader>();//Todo Try change
            builder.RegisterType<MSWordFileReader>().As<IFileReader>();//Todo Try change

            builder.RegisterType<LengthMoreThanValidator>().As<IElementValidator>();
            builder.RegisterType<YandexBoringPartsOfSpeechValidator>().As<IElementValidator>()
                .WithParameter(new NamedParameter("partsOfSpeech", boringSpeech));
            builder.RegisterType<BoringWordsValidator>().As<IElementValidator>()
                .WithParameter(new NamedParameter("words", boringWords));

            builder.RegisterType<YandexElementParser>().As<ElementParserWithRules>().As<BaseElementParser>();
            builder.RegisterType<YandexToolUser>()
                .WithParameters(new[]
                {
                    new NamedParameter("filePath", FilePath),
                    new NamedParameter("toolName", FileName)
                });
        }
    }
}