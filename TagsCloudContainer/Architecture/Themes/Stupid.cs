using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TagsCloudContainer.Architecture.Tags;

namespace TagsCloudContainer.Architecture.Themes
{
    public class Stupid : ITheme
    {
        public Dictionary<TagType, TagAppearance> TagAppearanceByType => tagAppearanceByType;

        private static readonly Dictionary<TagType, TagAppearance> tagAppearanceByType = new Dictionary<TagType, TagAppearance>()
        {
            {TagType.Biggest, BiggestTagAppearence},
            {TagType.Big, BigTagAppearence},
            {TagType.Medium, MediumTagAppearence},
            {TagType.Small, SmallTagAppearence},
        };

        private static string FontName => "Comic Sans MS";
        
        private static TagAppearance BiggestTagAppearence => new TagAppearance(
            new SolidBrush(Color.FromArgb(255, 0, 255)), FontName, 64);
        private static TagAppearance BigTagAppearence => new TagAppearance(
            new SolidBrush(Color.FromArgb(0, 255, 0)), FontName, 32);
        private static TagAppearance MediumTagAppearence => new TagAppearance(
            new SolidBrush(Color.FromArgb(0, 0, 128)), FontName, 16);
        private static TagAppearance SmallTagAppearence => new TagAppearance(
            new SolidBrush(Color.FromArgb(0, 0, 255)), FontName, 12);

        public TagAppearance GetTagAppearanceByType(TagType type)
        {
            return tagAppearanceByType[type];
        }

        public Brush BackgroundColor => Brushes.Yellow;
    }
}