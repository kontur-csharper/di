﻿using CommandLine;

namespace TagsCloudContainer
{
    public class ConsoleUserHandler : IUserHandler
    {
        private readonly string[] args;

        public ConsoleUserHandler(string[] args)
        {
            this.args = args;
        }

        public InputInfo GetInputInfo()
        {
            string fileName = "";
            int maxCnt = int.MaxValue;
            string imageFormat = "png";
            var a = Parser.Default.ParseArguments<StandartOptions>(args).
                WithParsed(opts =>
                {
                    fileName = opts.File;
                    maxCnt = opts.MaxCnt;
                    imageFormat = opts.Format;
                });
            return new InputInfo(fileName, maxCnt, imageFormat);
        }
    }
}