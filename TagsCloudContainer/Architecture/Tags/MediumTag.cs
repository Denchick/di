using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.Architecture.Tags
{
    class MediumTag : Tag
    {
        public override Brush Brush => new SolidBrush(Color.FromArgb(212, 85, 0));
        public override Font Font => new Font("Arial", 16.0f);
    }
}
