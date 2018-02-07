using System.Collections;
using System.Collections.Generic;
using TagsCloudContainer.Architecture;

namespace TagsCloudContainer
{
    public interface ITagsDrawer
    {
        Result<None> Draw();
    }
}