using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TagsCloudContainer.Architecture.Tags;
using TagsCloudContainer.Architecture.Themes;
using TagsCloudContainer.Utils;

namespace TagsCloudContainer.Architecture
{
    public class CircularCloudLayouter : ICloudLayouter
    {
        private Vector CloudCenter { get; set; }
        private List<Rectangle> Rectangles = new List<Rectangle>();
        private IWordsParser WordsParser { get; set; }
        private IEnumerable<Tag> Tags { get; }
        public int Width { get;}
        public int Height { get; }
        public ITheme Theme { get; }

        
        public CircularCloudLayouter(IWordsParser parser, ISettings settings)
        {
            CloudCenter = new Vector(settings.ImageSettings.Width / 2, settings.ImageSettings.Height/ 2);
            Width = settings.ImageSettings.Width;
            Height = settings.ImageSettings.Height;
            WordsParser = parser;
            Theme = settings.ImageSettings.Theme;
            Tags = MakeTagsFromTuples();
        }

        private Rectangle PutNextRectangle(Size rectangleSize)
        {
            var rectangleVector = Rectangles.Count == 0
                ? CloudCenter - new Vector(rectangleSize.Width, rectangleSize.Height) / 2
                : GetRectanglesVector(rectangleSize);
            var rectangle = new Rectangle(rectangleVector.X, rectangleVector.Y, rectangleSize.Width,
                rectangleSize.Height);
            Rectangles.Add(rectangle);
            return rectangle;
        }

        private Vector GetRectanglesVector(Size rectangleSize)
        {
            var radius = Math.Min(Rectangles.First().Width, Rectangles.First().Height);
            var step = 1;
            var rnd = new Random();
            while (true)
            {
                for (var offsetX = -radius; offsetX < radius; offsetX++)
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

        private bool CouldPutRectangle(Vector rectangleVector, Size rectangleSize)
        {
            if (rectangleVector.X < 0 || rectangleVector.Y < 0 || 
                rectangleVector.X + rectangleSize.Width > Width || rectangleVector.Y + rectangleSize.Height > Height)
                return false;
            var potentialRectangle = new Rectangle(rectangleVector.ToPoint(), rectangleSize);
            return Rectangles
                .All(rectangle => !rectangle.IntersectsWith(potentialRectangle));
        }

        private IEnumerable<Tag> MakeTagsFromTuples()
        {
            var pairs = WordsParser.Parse();
            var tags = new List<Tag>();
            var fifteenPercent = (int)(pairs.Count * 0.15);
            var thirtyFivePercent = (int)(pairs.Count * 0.35);

            tags.Add(new Tag(TagType.Biggest) { Text = pairs.First().Item1 });

            tags.AddRange(pairs
                .Skip(1)
                .Take(fifteenPercent)
                .Select(e => new Tag(TagType.Big) { Text = e.Item1 })
                .ToList());

            tags.AddRange(pairs
                .Skip(1 + fifteenPercent)
                .Take(thirtyFivePercent)
                .Select(e => new Tag(TagType.Medium) { Text = e.Item1 })
                .ToList());

            tags.AddRange(pairs
                .Skip(1 + fifteenPercent + thirtyFivePercent)
                .Select(e => new Tag(TagType.Small) { Text = e.Item1 })
                .ToList());

            return tags;
        }

        public IEnumerable<Tag> GetLayoutedTags()
        {
            foreach (var tag in Tags)
            {
                var tagFont = Theme.GetTagAppearanceByType(tag.Type).Font;
                var tagSize = TextRenderer.MeasureText(tag.Text, tagFont);
                tag.Rectangle = PutNextRectangle(tagSize);
                yield return tag;
            }
        }
    }
}
