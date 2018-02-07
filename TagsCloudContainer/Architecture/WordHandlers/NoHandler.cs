using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer.Architecture
{
    public class NoHandler : IWordHandler 
    {
        public Result<string> Handle(string word)
        {
            return Result.Ok(word);
        }
        
        public IEnumerable<string> Handle(IEnumerable<string> words)
        {
            return words.Select(w => Handle(w).Value);
        }
    }
}