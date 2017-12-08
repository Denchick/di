using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudContainer.Architecture.Tags;

namespace TagsCloudContainer.Architecture
{
    public class Tag
    {
        public string Text { get; set; }
        public Rectangle Rectangle { get; set; }
        public TagType Type { get; private set; }
        
        public Tag(TagType type)
        {
            Type = type;
        }
    }
}
