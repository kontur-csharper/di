﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace TagsCloudVisualization.WordsFileReading
{
    public class TextFileReader : IFileReader
    {
        public IEnumerable<string> ReadAllWords(string fileName)
        {
            using (var reader = new StreamReader(fileName, Encoding.UTF8))
            {
                while (true)
                {
                    var line = reader.ReadLine();
                    if (line == null)
                        yield break;
                    yield return line.Trim();
                }
            }
        }

        public string[] SupportedTypes()
        {
            return new[] {"txt"};
        }
    }
}
