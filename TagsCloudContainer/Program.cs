using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autofac;
using Autofac.Core;
using CommandLine;
using TagsCloudContainer.Architecture;
using TagsCloudContainer.Architecture.TagsMakers;
using TagsCloudContainer.Architecture.Themes;
using TagsCloudContainer.Properties;
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

            var imageSettings = new ImageSettings(
                options.ImageFilename, options.Height, options.Width, GetThemeByName(options.Theme), options.Gui);
            var wordsParserSettings = new WordsParserSettings(options.Count);
            var fileReaderSettings = new FileReaderSettings(options.InputFileName);
            
            var builder = new ContainerBuilder();
            builder.RegisterInstance(imageSettings)
                .As<ImageSettings>();
            builder.RegisterInstance(wordsParserSettings)
                .As<WordsParserSettings>();
            builder.RegisterInstance(fileReaderSettings)
                .As<FileReaderSettings>();
            builder.RegisterType<AppSettings>()
                .As<ISettings>();
            
            builder.RegisterType<SimpleFileReader>()
                .As<IFileFormatReader>();
            builder.RegisterType<NoHandler>()
                .As<IWordHandler>();
            builder.RegisterType<SimpleWordsParser>()
                .As<IWordsParser>();
            builder.RegisterType<StandardTagsBuilder>()
                .As<ITagsBuilder>();
            builder.RegisterType<CircularCloudLayouter>()
                .As<ICloudLayouter>();
            builder.RegisterType<BitmapDrawer>()
                .As<ITagsDrawer>();            

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