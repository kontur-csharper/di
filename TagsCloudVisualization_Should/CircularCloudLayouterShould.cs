using System;
using System.Collections.Generic;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.CloudLayouters;
using TagsCloudVisualization.Configs;
using TagsCloudVisualization.PointProviders;

namespace TagsCloudVisualization_Should
{
    public class CircularCloudLayouterShould
    {
        private List<Rectangle> actualRectangles;

        private IConfig config;

        [SetUp]
        public void SetUp()
        {
            config = new Config();
            config.SetValues(new Font(FontFamily.GenericMonospace, 25),
                new Point(1500, 1500), Color.Blue, new Size(1500, 1500), new HashSet<string>());
        }

        [Test]
        public void PutNextRectangle_ThrowArgumentException_SizeOfRectangleHaveNegativeValue()
        {
            var center = new Point(100, 100);
            config.SetValues(new Font(FontFamily.GenericMonospace, 25),
                center, Color.Blue, new Size(1500, 1500), new HashSet<string>());
            var pointProvider = new PointProvider(config);
            var cloud = new CircularCloudLayouter(pointProvider);

            Action act = () => cloud.PutNextRectangle(new Size(-1, -1));

            act.ShouldThrow<ArgumentException>().WithMessage("Width or height of size was negative");
        }

        [Test]
        public void PutNextRectangle_ReturnSameRectangle_OneRectangle()
        {
            var center = new Point(40, 40);
            config.SetValues(new Font(FontFamily.GenericMonospace, 25),
                center, Color.Blue, new Size(1500, 1500), new HashSet<string>());
            var pointProvider = new PointProvider(config);
            var expectedRectangle = new Rectangle(new Point(40, 40), new Size(30, 30));
            var cloud = new CircularCloudLayouter(pointProvider);

            var actual = cloud.PutNextRectangle(new Size(30, 30));

            actual.ShouldBeEquivalentTo(expectedRectangle);
        }

        [Test]
        public void Rectangles_CountIsTen_RandomTenRectangles()
        {
            var rnd = new Random();
            var center = new Point(500, 500);
            config.SetValues(new Font(FontFamily.GenericMonospace, 25),
                center, Color.Blue, new Size(1500, 1500), new HashSet<string>());
            var pointProvider = new PointProvider(config);
            var cloud = new CircularCloudLayouter(pointProvider);
            const int expectedLength = 10;

            for (var i = 0; i < 10; i++)
            {
                var size = new Size(rnd.Next(10, 200), rnd.Next(10, 200));
                cloud.PutNextRectangle(size);
            }

            var actualLength = cloud.Rectangles.Count;


            actualLength.Should().Be(expectedLength);
        }

        [Test]
        public void Rectangles_SameOrderLikeAdded_ThreeRectangles()
        {
            var center = new Point(500, 500);
            config.SetValues(new Font(FontFamily.GenericMonospace, 25),
                center, Color.Blue, new Size(1500, 1500), new HashSet<string>());
            var pointProvider = new PointProvider(config);
            var cloud = new CircularCloudLayouter(pointProvider);
            var expectedRectangles = new List<Rectangle>
            {
                new Rectangle(new Point(500, 500), new Size(30, 30)),
                new Rectangle(new Point(530, 493), new Size(40, 40)),
                new Rectangle(new Point(510, 530), new Size(20, 20))
            };

            cloud.PutNextRectangle(new Size(30, 30));
            cloud.PutNextRectangle(new Size(40, 40));
            cloud.PutNextRectangle(new Size(20, 20));
            actualRectangles = cloud.Rectangles;

            actualRectangles.ShouldAllBeEquivalentTo(expectedRectangles);
        }
    }
}