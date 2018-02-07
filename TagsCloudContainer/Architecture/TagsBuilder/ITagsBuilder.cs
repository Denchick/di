using System.Collections.Generic;

namespace TagsCloudContainer.Architecture.TagsMakers
{
    public interface ITagsBuilder
    {
        IEnumerable<Result<Tag>> Build();
    }
}