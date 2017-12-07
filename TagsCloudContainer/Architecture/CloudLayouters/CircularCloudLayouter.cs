using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TagsCloudContainer.Architecture.Tags;
using TagsCloudContainer.Utils;

namespace TagsCloudContainer.Architecture
{
    public class CircularCloudLayouter : ICloudLayouter
    {
        public Vector CloudCenter { get; set; }
        public List<Rectangle> Rectangles = new List<Rectangle>();
        public IWordsParser WordsParser { get; set; }
        public IEnumerable<Tag> Tags { get; }

        
        public CircularCloudLayouter(IWordsParser parser)
        {
            CloudCenter = new Vector(new Point(300, 300));
            WordsParser = parser; 
            Tags = MakeTagsFromTuples();
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            var rectangleVector = Rectangles.Count == 0
                ? CloudCenter - new Vector(rectangleSize.Width, rectangleSize.Height) / 2
                : GetRectanglesVector(rectangleSize);
            var rectangle = new Rectangle(rectangleVector.X, rectangleVector.Y, rectangleSize.Width,
                rectangleSize.Height);
            Rectangles.Add(rectangle);
            return rectangle;
        }

        public Vector GetRectanglesVector(Size rectangleSize)
        {
            var radius = Math.Min(Rectangles.First().Width, Rectangles.First().Height);
            var step = 1;
            while (true)
            {
                for (int offsetX = -radius; offsetX < radius; offsetX++)
                {
                    var offsetY = (int)Math.Round(Math.Sqrt(radius * radius - offsetX * offsetX));
                    var rectangleVector1 = CloudCenter - new Vector(offsetX, offsetY) / 2;
                    var rectangleVector2 = CloudCenter - new Vector(offsetX, -offsetY) / 2;
                    if (CouldPutRectangle(rectangleVector1, rectangleSize))
                        return rectangleVector1;
                    if (CouldPutRectangle(rectangleVector2, rectangleSize))
                        return rectangleVector2;
                }
                radius += step;
            }
        }

        public bool CouldPutRectangle(Vector rectangleVector, Size rectangleSize)
        {
            if (rectangleVector.X < 0 || rectangleVector.Y < 0)
                return false;
            var potentialRectangle = new Rectangle(rectangleVector.ToPoint(), rectangleSize);
            foreach (var rectangle in Rectangles)
                if (rectangle.IntersectsWith(potentialRectangle))
                    return false;
            return true;
        }

        public List<Tag> MakeTagsFromTuples()
        {
            var pairs = WordsParser.Parse();
            var tags = new List<Tag>();
            var fifteenPercent = (int)(pairs.Count * 0.15);
            var thirtyFivePercent = (int)(pairs.Count * 0.35);

            tags.Add(new BiggestTag() { Text = pairs.First().Item1 });

            tags.AddRange(pairs
                .Skip(1)
                .Take(fifteenPercent)
                .Select(e => new BigTag() { Text = e.Item1 })
                .ToList());

            tags.AddRange(pairs
                .Skip(1 + fifteenPercent)
                .Take(thirtyFivePercent)
                .Select(e => new MediumTag() { Text = e.Item1 })
                .ToList());

            tags.AddRange(pairs
                .Skip(1 + fifteenPercent + thirtyFivePercent)
                .Select(e => new SmallTag() { Text = e.Item1 })
                .ToList());

            return tags;
        }

        public IEnumerable<Tag> GetLayoutedTags()
        {
            foreach (var tag in Tags)
            {
                var tagSize = TextRenderer.MeasureText(tag.Text, tag.Font);
                tag.Rectangle = PutNextRectangle(tagSize);
                yield return tag;
            }
        }
    }
}
