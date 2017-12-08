using System.Collections.Generic;
using NUnit.Framework;
using TagsCloudContainer.Architecture.Themes;

namespace TagsCloudContainer.Architecture
{
    public interface ICloudLayouter
    {
        IEnumerable<Tag> GetLayoutedTags();
        int Width { get; }
        int Height { get; }
        ITheme Theme { get; }
    }
}