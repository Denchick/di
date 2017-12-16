using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Architecture;
using TagsCloudContainer.Architecture.Themes;
using TagsCloudContainer.Utils;

namespace TagsCloudContainer.Tests
{
    public class CloudLayouterTests
    {
        private ImageSettings DefaultImageSettings { get; set; }
        private ImageSettings PerfomanceImageSettings { get; set; }

        
        [SetUp]
        public void SetUp()
        {
            DefaultImageSettings = new ImageSettings("test.png", 100, 100, new Stupid(), false);
            PerfomanceImageSettings = new ImageSettings("test.png", 1000, 1000, new Stupid(), false);
        }
        
        [TestCase(10, 20)]
        [TestCase(20, 10)]
        [TestCase(3, 5)]
        public void Cloudlayoter_SetUpCenterCorrectly(int width, int height)
        {
            var center = new Point(width / 2, height / 2);
            DefaultImageSettings.Width = width;
            DefaultImageSettings.Height = height;

            var cloud = new CircularCloudLayouter(DefaultImageSettings);

            cloud.CloudCenter.Should().Be(new Vector(center));
        }

        public void CloudLayouterCorrect_WhenPuttingOneRectangle(
            int cloudCenterX, int cloudCenterY, int rectangleWidth, int rectangleHeight)
        {
            var cloudCenter = new Point(cloudCenterX, cloudCenterY);
            var rectangleSize = new Size(rectangleWidth, rectangleHeight);

            var layouter = new CircularCloudLayouter(DefaultImageSettings);
            layouter.PutNextRectangle(rectangleSize);

            CheckForCorrectLayout(new List<Size>() { rectangleSize }, layouter.Rectangles);
        }

        [TestCase(0, 0)]
        [TestCase(0, 0, 10, 20)]
        [TestCase(10, 10, 10, 20)]
        [TestCase(0, 0, 10, 10)]
        [TestCase(10, 10, 10, 10)]
        [TestCase(0, 0, 1, 1)]
        [TestCase(0, 0, 10, 20, 20, 10)]
        [TestCase(-7, -3, 10, 20, 20, 10)]
        [TestCase(7, 3, 10, 20, 20, 10)]
        [TestCase(0, 0, 10, 2, 5, 10, 14, 2, 8, 20, 21, 10, 11, 13, 2, 18, 3, 23, 10, 20, 14, 2)]
        public void CloudLayouterCorrect_IfPuttingAnyRectangles(int cloudCenterX, int cloudCenterY, params int[] sizes)
        {
            if (sizes.Length % 2 != 0)
                throw new Exception("One param has no pair");
            var cloudCenter = new Point(cloudCenterX, cloudCenterY);
            var rectanglesSizes = new List<Size>();
            for (int i = 0; i < sizes.Length; i += 2)
                rectanglesSizes.Add(new Size(sizes[i], sizes[i + 1]));

            var layouter = new CircularCloudLayouter(DefaultImageSettings);
            foreach (var size in rectanglesSizes)
                layouter.PutNextRectangle(size);

            CheckForCorrectLayout(rectanglesSizes, layouter.Rectangles);
        }

        private static void CheckForCorrectLayout(List<Size> rectanglesSizes, List<Rectangle> layout)
        {
            layout.Should().HaveCount(rectanglesSizes.Count);

            foreach (var rectangle in layout)
                rectanglesSizes.Should().Contain(rectangle.Size);

            foreach (var rectangle1 in layout)
                foreach (var rectangle2 in layout)
                    if (rectangle2 != rectangle1)
                        rectangle1.IntersectsWith(rectangle2).Should().BeFalse();
        }

        [TestCase(10, 1000)]
        [TestCase(1000, 10)]
        public void PerfomanceTest(int maxRectangleMeasure, int rectanglesCount)
        {
            var cloudCenter = new Point(0, 0);
            var rnd = new Random();
            var sizes = new List<Size>();

            DefaultImageSettings.Width = 100000;
            DefaultImageSettings.Height = 100000;
            
            var layouter = new CircularCloudLayouter(DefaultImageSettings);
            for (int i = 0; i < rectanglesCount * 2; i++)
            {
                var rectangleSize = new Size(rnd.Next(maxRectangleMeasure), rnd.Next(maxRectangleMeasure));
                sizes.Add(rectangleSize);
                layouter.PutNextRectangle(rectangleSize);
            }
        }
        
    }
}
