using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.Architecture.Tags
{
    class SmallTag : Tag
    {
        public override Brush Brush => new SolidBrush(Color.FromArgb(156, 89, 44));
        public override Font Font => new Font("Arial", 12);
    }
}
