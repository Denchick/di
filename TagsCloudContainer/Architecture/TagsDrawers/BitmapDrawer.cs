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

        public BitmapDrawer(ImageSettings settings, ITagsBuilder tagsBuilder)
        {
            Size = new Size(settings.Width, settings.Height);
            Offset = new Point(0, 0);
            Theme = settings.Theme;
            Filename = settings.Filename;
            TagsBuilder = tagsBuilder;
        }

        public Result<None> Draw()
        {
            Bitmap = new Bitmap(Size.Width, Size.Height);
            FillBitmapsBackground(Theme.BackgroundColor);
            DrawTagsOnBitmap();
            SaveBitmap();
            return Result.Ok();
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
                var tagFont = Theme.GetTagAppearanceByType(tag.Value.Type).Font;
                var tagBrush = Theme.GetTagAppearanceByType(tag.Value.Type).Brush;
                var tagSize = TextRenderer.MeasureText(tag.Value.Text, tagFont);
                var sourceRectangle = tag.Value.Rectangle;
                tag.Value.Rectangle =
                    new Rectangle(new Point(sourceRectangle.Left - Offset.X, sourceRectangle.Top - Offset.Y),
                        sourceRectangle.Size);
                graphics.DrawString(tag.Value.Text, tagFont, tagBrush, tag.Value.Rectangle.Location);
            }
        }

        private void SaveBitmap()
        {
            Bitmap.Save(Filename, ImageFormat.Png);
        }

    }
}
