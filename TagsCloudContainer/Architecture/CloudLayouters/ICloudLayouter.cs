using System.Collections.Generic;
using NUnit.Framework;

namespace TagsCloudContainer.Architecture
{
    public interface ICloudLayouter
    {
        IEnumerable<Tag> GetLayoutedTags();
    }
}