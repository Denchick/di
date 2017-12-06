using System.Collections.Generic;
using NUnit.Framework;

namespace TagsCloudContainer.Architecture
{
    public interface ICloudLayouter
    {
        void SetRectangeForEachTag(List<Tag> tags);
        IEnumerable<Tag> GetTags();
    }
}