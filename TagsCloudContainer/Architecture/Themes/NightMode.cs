using System.Collections.Generic;
using System.Drawing;
using TagsCloudContainer.Architecture.Tags;

namespace TagsCloudContainer.Architecture.Themes
{
    public class NightMode : ITheme
    {
        public Dictionary<TagType, TagAppearance> TagAppearanceByType => tagAppearanceByType;

        private static readonly Dictionary<TagType, TagAppearance> tagAppearanceByType = new Dictionary<TagType, TagAppearance>()
        {
            {TagType.Biggest, BiggestTagAppearence},
            {TagType.Big, BigTagAppearence},
            {TagType.Medium, MediumTagAppearence},
            {TagType.Small, SmallTagAppearence},
        };
        
        
        private static TagAppearance BiggestTagAppearence => new TagAppearance(
            Brushes.White, "Arial", 64);
        private static TagAppearance BigTagAppearence => new TagAppearance(
            new SolidBrush(Color.FromArgb(255, 102, 0)), "Arial", 32);
        private static TagAppearance MediumTagAppearence => new TagAppearance(
            new SolidBrush(Color.FromArgb(212, 85, 0)), "Arial", 16);
        private static TagAppearance SmallTagAppearence => new TagAppearance(
            new SolidBrush(Color.FromArgb(156, 89, 44)), "Arial", 12);

        public TagAppearance GetTagAppearanceByType(TagType type)
        {
            return tagAppearanceByType[type];
        }

        public Brush BackgroundColor => Brushes.Black;
    }
}