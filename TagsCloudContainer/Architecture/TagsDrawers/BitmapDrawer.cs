using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TagsCloudContainer.Architecture;
using TagsCloudContainer.Architecture.Themes;

namespace TagsCloudContainer
{
    public class BitmapDrawer : ITagsDrawer
    {
        private List<Tag> Tags { get; set; }
        private Bitmap Bitmap { get; set; }
        private Size Size { get; set; }
        private Point Offset { get; set; }
        private string Filename { get; set;}
        private ITheme Theme { get; set; }

        public BitmapDrawer(ICloudLayouter layouter, string filename)
        {
            Tags = layouter.GetLayoutedTags().ToList();
            Size = new Size(layouter.Width, layouter.Height);
            Offset = new Point(0, 0);
            Theme = layouter.Theme;
            Filename = filename;
        }


        public void Draw()
        {
            Bitmap = new Bitmap(Size.Width, Size.Height);
            FillBitmapsBackground(Theme.BackgroundColor);
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
                var tagFont = Theme.GetTagAppearanceByType(tag.Type).Font;
                var tagBrush = Theme.GetTagAppearanceByType(tag.Type).Brush;
                var tagSize = TextRenderer.MeasureText(tag.Text, tagFont);
                var sourceRectangle = tag.Rectangle;
                tag.Rectangle =
                    new Rectangle(new Point(sourceRectangle.Left - Offset.X, sourceRectangle.Top - Offset.Y),
                        sourceRectangle.Size);
                graphics.DrawString(tag.Text, tagFont, tagBrush, tag.Rectangle.Location);
            }
        }

        private void SaveBitmap()
        {
            Bitmap.Save(Filename, ImageFormat.Png);
        }

    }
}
