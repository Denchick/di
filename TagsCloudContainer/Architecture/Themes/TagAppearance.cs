using System;
using System.Drawing;

namespace TagsCloudContainer.Architecture.Themes
{
    public class TagAppearance
    {
        private int fontSize;
        public Brush Brush { get; }
        private string FontFamily { get; }

        public Font Font => new Font(FontFamily, FontSize);

        private int FontSize
        {
            get => fontSize;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Font size cannot be less or equal zero.");
                fontSize = value;
            }
        }

        public TagAppearance(Brush brush, string fontFamily, int fontSize)
        {
            Brush = brush;
            FontFamily = fontFamily;
            FontSize = fontSize;
        }
    }
    
}