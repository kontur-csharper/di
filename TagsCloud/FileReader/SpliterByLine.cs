﻿using System;
using System.Collections.Generic;
using TagsCloud.Interfaces;
using System.Linq;

namespace TagsCloud.FileReader
{
    public class SpliterByLine : ITextSpliter
    {
        private string splitChar = Environment.NewLine;

        public IEnumerable<string> SplitText(string text)
        {
            if (text == null)
                throw new ArgumentException();
            return text.Split(splitChar).Where(line => !string.IsNullOrWhiteSpace(line));
        }
    }
}
