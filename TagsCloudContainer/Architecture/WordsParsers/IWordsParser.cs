using System.Collections.Generic;

namespace TagsCloudContainer
{
    public interface IWordsParser
    {
        List<(string, int)> Parse();
    }
}