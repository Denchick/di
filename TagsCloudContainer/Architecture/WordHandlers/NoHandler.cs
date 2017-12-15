using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer.Architecture
{
    public class NoHandler : IWordHandler 
    {
        public string Handle(string word)
        {
            return word;
        }
        
        public IEnumerable<string> Handle(IEnumerable<string> words)
        {
            return words.Select(Handle);
        }
    }
}