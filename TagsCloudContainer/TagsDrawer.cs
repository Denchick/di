using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TagsCloudContainer.Architecture;

namespace TagsCloudContainer
{
    public class TagsDrawer
    {
        public List<Tag> Tags { get; }
        public Bitmap Bitmap { get; }
        public Size Size { get; private set; }
        public Point Offset { get; private set; }
        public string Filename { get; }

        public TagsDrawer(string filename, List<Tag> tags)
        {
            Filename = filename;
            Tags = tags;
            CalculateOffsetAndSizeOfBitmap();
            Bitmap = new Bitmap(Size.Width, Size.Height);

            FillBitmapsBackground(Brushes.Black);
            DrawTagsOnBitmap();
            SaveBitmap();
        }

        private void FillBitmapsBackground(Brush brush)
        {
            var graphics = Graphics.FromImage(Bitmap);
            graphics.FillRectangle(brush, 0, 0, Bitmap.Width, Bitmap.Height);
        }

        private void DrawTagsOnBitmap()
        {
            var graphics = Graphics.FromImage(Bitmap);
            foreach (var tag in Tags)
            {
                var tagSize = TextRenderer.MeasureText(tag.Text, tag.Font);
                var sourceRectangle = tag.Rectangle;
                tag.Rectangle =
                    new Rectangle(new Point(sourceRectangle.Left - Offset.X, sourceRectangle.Top - Offset.Y),
                        sourceRectangle.Size);
                graphics.DrawString(tag.Text, tag.Font, tag.Brush, tag.Rectangle.Location);
            }
        }

        private void CalculateOffsetAndSizeOfBitmap()
        {
            var leftBorder = Tags
                .Select(e => e.Rectangle.Left)
                .Min();
            var rightBorder = Tags
                .Select(e => e.Rectangle.Right)
                .Max();
            var topBorder = Tags
                .Select(e => e.Rectangle.Top)
                .Min();
            var bottomBorder = Tags
                .Select(e => e.Rectangle.Bottom)
                .Max();

            Size = new Size(rightBorder - leftBorder, bottomBorder - topBorder);
            Offset = new Point(leftBorder, topBorder);
        }

        public void SaveBitmap()
        {
            Bitmap.Save(Filename);
        }
    }
}
