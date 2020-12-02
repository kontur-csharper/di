﻿using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization;

namespace TagsCloudVisualization_Should
{
    public class WordToRectangleConverterShould
    {
        [Test]
        public void ConvertWords_CorrectListWithTags_NormalWords()
        {
            var config = new Config();
            config.SetValues(new Font(FontFamily.GenericMonospace, 25), 
                new Point(1500, 1500), Color.Blue);
            var pointProvider = new PointProvider(config);
            var cloud = new CircularCloudLayouter(pointProvider);
            var converter = new WordsToRectanglesConverter(cloud, config);
            var words = new List<string>{"он", "пошел"};
            var expectedTags = (new List<CloudTag>
            {
                new CloudTag(new Rectangle(1500, 1500, 52, 41), "он"),
                new CloudTag(new Rectangle(1500, 1459, 114, 41), "пошел")
            }).Cast<ICloudTag>().ToList();

            var actualTags = converter.ConvertWords(words);

            actualTags.Should().BeEquivalentTo(actualTags);
        }
    }
}