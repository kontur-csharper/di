using System;
using System.Collections.Generic;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.Configs;
using TagsCloudVisualization.PointProviders;

namespace TagsCloudVisualization_Should
{
    public class PointProviderShould
    {
        private IConfig config;

        [SetUp]
        public void SetUp()
        {
            config = new Config();
            config.SetValues(new Font(FontFamily.GenericMonospace, 25),
                new Point(1500, 1500), Color.Blue, new Size(1500, 1500), new HashSet<string>());
        }

        [Test]
        public void GetPoint_ReturnPoint_AfterCallingMethod()
        {
            var center = new Point(500, 500);
            config.SetValues(new Font(FontFamily.GenericMonospace, 25),
                center, Color.Blue, new Size(1500, 1500), new HashSet<string>());
            var pointProvider = new PointProvider(config);
            var expectedPoint = new Point(500, 500);

            var actualPoint = pointProvider.GetPoint();

            actualPoint.ShouldBeEquivalentTo(expectedPoint);
        }

        [Test]
        public void CreatePointProvider_ThrowArgumentException_CenterWithNegativeXOrY()
        {
            var center = new Point(-1, -1);
            config.SetValues(new Font(FontFamily.GenericMonospace, 25),
                center, Color.Blue, new Size(1500, 1500), new HashSet<string>());
            Action act = () => new PointProvider(config);

            act.ShouldThrow<ArgumentException>().WithMessage("X or Y of center was negative");
        }
    }
}