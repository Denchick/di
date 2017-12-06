using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.Architecture.Tags
{
    class BigTag : Tag
    {
        public override Brush Brush => new SolidBrush(Color.FromArgb(255, 102, 0));
        public override Font Font => new Font("Arial", 32.0f);
    }
}
