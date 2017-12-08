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
using CommandLine;
using TagsCloudContainer.Architecture;
using TagsCloudContainer.Architecture.Themes;
using TagsCloudContainer.Utils;

namespace TagsCloudContainer
{
    class Program
    {
        public static void Main(string[] args)
        {
            var options = new Options();
            if (!Parser.Default.ParseArguments(args, options))
            {
                options.GetUsage();
                return;
            }
            
            var builder = new ContainerBuilder();
            builder.RegisterType<SimpleFileReader>()
                .As<IFileFormatReader>()
                .WithParameter("filename", options.InputFileName);
            builder.RegisterType<SimpleWordsParser>()
                .WithParameter("countWordsToParse", options.Count)
                .As<IWordsParser>();
            builder.RegisterType<CircularCloudLayouter>()
                .As<ICloudLayouter>()
                .WithParameter("theme", GetThemeByName(options.Theme))
                .WithParameter("height", options.Height)
                .WithParameter("width", options.Width);
            builder.RegisterType<BitmapDrawer>()
                .As<ITagsDrawer>()
                .WithParameter("filename", options.ImageFilename);
            

            var container = builder.Build();
            var tagsDrawer = container.Resolve<ITagsDrawer>();
            tagsDrawer.Draw();
        }

        private static ITheme GetThemeByName(string themeName)
        {
            if (themeName == "NightMode")
                return new NightMode();
            return new Stupid();
        }
    }
}