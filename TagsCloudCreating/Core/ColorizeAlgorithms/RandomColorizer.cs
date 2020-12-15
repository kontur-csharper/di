using System;
using System.ComponentModel;
using System.Drawing;
using TagsCloudCreating.Contracts;

namespace TagsCloudCreating.Core.ColorizeAlgorithms
{
    public class RandomColorizer : IColorizer
    {
        private readonly Random rnd = new Random();

        [Browsable(false)] public string Name => "Random colorizer";
        public Color Paint() => Color.FromArgb(rnd.Next(255), rnd.Next(255), rnd.Next(255));
    }
}