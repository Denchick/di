using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.Architecture
{
    public class Tag
    {
        public string Text { get; set; }
        public virtual Brush Brush { get; set; }
        public virtual Font Font { get; set; }
        public virtual Rectangle Rectangle { get; set; }
    }
}
