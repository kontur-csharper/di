using System;
using System.Linq;
using System.Collections.Generic;
using CommandLine;
using TagCloud.TextConverters.TextReaders;
using TagCloud.TextConverters.TextProcessors;
using TagCloud.WordsMetrics;
using TagCloud.Visualization;
using TagCloud.Visualization.WordsColorings;
using TagCloud.CloudLayoters;
using TagCloud.PointGetters;

namespace TagCloud.Clients
{
    internal class ConsoleClient : IClient
    {
        private ITextProcessor processor;
        private IWordsMetric metric;
        private ICloudLayoter layoter;

        private VisualizationInfo vizInfo;

        public void Run()
        {
            Console.WriteLine("Hello, I'm your personal visualization client");
            while (true)
            {
                Console.WriteLine("Write path to file with text or \"exit\" to exit");
                var answear = Console.ReadLine();
                if (answear == "exit")
                    break;
                var text = TextReaderAssosiation.GetTextReader(answear).ReadText(answear);
                if (text == null)
                {
                    Console.WriteLine("something was wrong, please, try again");
                    continue;
                }
                Console.WriteLine("write params cloud configuration");
                var answears = new List<string>() 
                { 
                    Console.ReadLine(), 
                    Console.ReadLine(), 
                    Console.ReadLine(), 
                    Console.ReadLine() 
                };
                Parser.Default.ParseArguments<OptionsTagInfo>(answears)
                    .WithParsed(SetTagInfo);
                if(processor == null)
                {
                    Console.WriteLine("Text Processod didn't set");
                    continue;
                }
                if (metric == null)
                {
                    Console.WriteLine("Words Metric didn't set");
                    continue;
                }
                if (layoter == null)
                {
                    Console.WriteLine("Cloud Layoter didn't set");
                    continue;
                }
                Console.WriteLine("write Visualizate configuration or 'end' if you finish write");
                answears.Clear();
                while (true)
                {
                    answear = Console.ReadLine();
                    if (answear == "end")
                        break;
                    answears.Add(answear);
                }
                Parser.Default.ParseArguments<OptionsVisualizate>(answears)
                    .WithParsed(SetVisualizateInfo);
                Console.WriteLine("write path file to save");
                answear = Console.ReadLine();
                Visualize(text, answear);
                Console.WriteLine("file save");
            }
        }

        private void SetTagInfo(OptionsTagInfo info)
        {
            processor = TextProcessorAssosiation
                .GetProcessor(SkipSpaces(info.Processor));
            metric = WordsMetricAssosiation
                .GetMetric(SkipSpaces(info.Metric));
            layoter = CloudLayoterAssosiation
                .GetCloudLayoter(SkipSpaces(info.Layoter), SkipSpaces(info.PointGetter));
        }

        private void SetVisualizateInfo(OptionsVisualizate info)
        {
            var size = VisualizationInfo.ReadSize(info.Size);
            var coloring = WordsColoringAssosiation.GetColoring(SkipSpaces(info.Coloring)) ?? 
                WordsColoringAssosiation.GetColoring("random");
            vizInfo = new VisualizationInfo(coloring, size, SkipSpaces(info.Font));
        }

        private string SkipSpaces(string str) => string.Join("", str.SkipWhile(c => c == ' '));

        public void Visualize(string text, string picturePath)
        {
            var tagCloud = AlgorithmTagCloud.GetTagCloud(text, layoter, processor, metric);
            TagCloudVisualization.Visualize(tagCloud, picturePath, vizInfo);
        }

        internal class OptionsTagInfo
        {
            [Option('m', "metric", Required = true, HelpText = "Name of metric. Write count")]
            public string Metric { get; set; }

            [Option('p', "processor", Required = true, 
                HelpText = "type of text processor, write " + TextProcessorAssosiation.paragraph + " or " + TextProcessorAssosiation.words)]
            public string Processor { get; set; }

            [Option('g', "getter", Required = true,
                HelpText = "type of point getter, write " + PointGetterAssosiation.circle + " or " + PointGetterAssosiation.spiral)]
            public string PointGetter { get; set; }

            [Option('l', "layoter", Required = true,
                HelpText = "type of layoter, write " + CloudLayoterAssosiation.density + " or " + CloudLayoterAssosiation.identity)]
            public string Layoter { get; set; }
        }

        internal class OptionsVisualizate
        {
            [Option('s', "size", Required = false, Default = "", 
                HelpText = "size tu cut picture, write two numbers")]
            public string Size { get; set; }

            [Option('f', "font", Required = false, Default = "Arial",
                HelpText = "font of words, write Arial, Calibri, ...")]
            public string Font { get; set; }

            [Option('c', "coloring", Required = false, Default = "random",
                HelpText = "coloring text, write red, geen, blue, black, random, multi, line red, line green, line blue, line random")]
            public string Coloring { get; set; }
        }
    }
}
