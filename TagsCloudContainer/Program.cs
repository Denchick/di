using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autofac;
using Autofac.Core;
using TagsCloudContainer.Architecture;
using TagsCloudContainer.Utils;

namespace TagsCloudContainer
{
    class Program
    {
        public static void Main()
        {
            var cloudCenter = new Point(300, 300);
            
            var builder = new ContainerBuilder();
            builder.RegisterType<FileReader>()
                .As<ITextReader>();
            builder.RegisterType<SimpleWordsParser>()
                .As<IWordsParser>();
            builder.RegisterType<CircularCloudLayouter>()
                .As<ICloudLayouter>()
                .WithParameter("center", cloudCenter);
            builder.RegisterType<BitmapDrawer>()
                .As<ITagsDrawer>()
                .WithParameter("filename", "image.bmp");

            var container = builder.Build();
            var tagsDrawer = container.Resolve<ITagsDrawer>();
            tagsDrawer.Draw();
        }
    }
}