using System.Collections.Generic;
using System.Drawing;
using TagsCloudContainer.Architecture.Tags;

namespace TagsCloudContainer.Architecture.Themes
{
    public interface ITheme
    {
        TagAppearance GetTagAppearanceByType(TagType type);
        Brush BackgroundColor { get; }
    }
}