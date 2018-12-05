﻿using System.Drawing;
using TagsCloudVisualization.Interfaces;

namespace TagsCloudVisualization
{
    public class ArialFontScheme : IFontScheme
    {
        public Font Process(FrequentedWord element)
        {
            return new Font("Arial", (element.Frequency + 1) * 10);
        }
    }
}