using System.Collections;
using System.Collections.Generic;
using TagsCloudContainer.Architecture;

namespace TagsCloudContainer
{
    public interface ITagsDrawer
    {
        void Draw(string filename, IEnumerable<Tag> tags);
    }
}