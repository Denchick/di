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
            
            var builder = new ContainerBuilder();
            var settings = AppSettings.FromArgs(args);
            builder.RegisterInstance(settings)
                .As<ISettings>();
            builder.Register(ctx => ctx.Resolve<ISettings>().ImageSettings);
            builder.Register(ctx => ctx.Resolve<ISettings>().FileReaderSettings);
            builder.Register(ctx => ctx.Resolve<ISettings>().WordsParserSettings);
            builder.RegisterType<FileReader>()
                .As<IFileFormatReader>();
            builder.RegisterType<NoHandler>()
                .As<IWordHandler>();
            builder.RegisterType<SimpleWordsParser>()
                .As<IWordsParser>();
            builder.RegisterType<TagsBuilder>()
                .As<ITagsBuilder>();
            builder.RegisterType<CircularCloudLayouter>()
                .As<ICloudLayouter>();
            builder.RegisterType<BitmapDrawer>()
                .As<ITagsDrawer>();            

            var container = builder.Build();
            var tagsDrawer = container.Resolve<ITagsDrawer>();
            tagsDrawer.Draw();
        }
    }
}