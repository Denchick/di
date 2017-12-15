using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TagsCloudContainer.Architecture;
using TagsCloudContainer.Architecture.TagsMakers;
using TagsCloudContainer.Architecture.Themes;

namespace TagsCloudContainer
{
    public class BitmapDrawer : ITagsDrawer
    {
        private List<Tag> Tags { get; }
        private Bitmap Bitmap { get; set; }
        private Size Size { get; }
        private Point Offset { get; }
        private string Filename { get; }
        private ITheme Theme { get; }
        private ITagsBuilder TagsBuilder { get; }

        public BitmapDrawer(ISettings settings, ITagsBuilder tagsBuilder)
        {
            Size = new Size(settings.ImageSettings.Width, settings.ImageSettings.Height);
            Offset = new Point(0, 0);
            Theme = settings.ImageSettings.Theme;
            Filename = settings.ImageSettings.Filename;
            TagsBuilder = tagsBuilder;
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
            foreach (var tag in TagsBuilder.Build())
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
