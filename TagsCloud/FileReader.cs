﻿using System.Collections.Generic;
using System.IO;
using System.Text;
using TagsCloudVisualization.Interfaces;

namespace TagsCloudVisualization
{
    public class FileReader : IFileReader
    {
        public string Path { get; set; }

        public IEnumerable<string> Read()
        {
            using (var sr = new StreamReader(Path, Encoding.Default))
            {
                return sr.ReadToEnd().Split(' ');
            }
        }
    }
}