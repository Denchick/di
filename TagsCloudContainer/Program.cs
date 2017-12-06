using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autofac;
using TagsCloudContainer.Architecture;
using TagsCloudContainer.Utils;

namespace TagsCloudContainer
{
    class Program
    {
        public static void Main()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<FileReader>().As<ITextReader>();
            builder.RegisterType<SimpleWordsParser>().As<IWordsParser>();
            builder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>();
            builder.RegisterType<BitmapDrawer>().As<ITagsDrawer>();

            var container = builder.Build();
            var cloudCenter = new Point(300, 300);
            container.Resolve<CircularCloudLayouter>(new NamedParameter("center", cloudCenter));
            container.Resolve<BitmapDrawer>(new NamedParameter("filename", "image.bmp"));
            var tagsDrawer = container.Resolve<BitmapDrawer>();
            tagsDrawer.Draw();
        }
    }
}