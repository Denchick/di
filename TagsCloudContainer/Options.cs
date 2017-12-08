using System.Drawing;
using CommandLine;
using CommandLine.Text;

namespace TagsCloudContainer
{
    class Options {        
        
        [Option('t', "text", Required = true,
            HelpText = "Текст, по которому строить облако тегов.")]
        public string InputFileName { get; set; }
        
        [Option('g', "gui", Required = false, DefaultValue = false,
            HelpText = "Построить облако в окне")]
        public bool Gui { get; set; }

        [Option('i', "image", Required = false, DefaultValue = "image.png",
            HelpText = "Имя файла, в который нужно сохранить изображение")]
        public string ImageFilename { get; set; }
        
        [Option('h', "height", Required = false, DefaultValue = "800",
            HelpText = "Высота изображения в пискелах")]
        public string Height { get; set; }
        
        [Option('w', "width", Required = false, DefaultValue = "800",
            HelpText = "Ширина изображения в пискелах")]
        public string Width { get; set; }
        
        
        [ParserState]
        public IParserState LastParserState { get; set; }

        [HelpOption]
        public string GetUsage() {
            var help = HelpText.AutoBuild(this,
                (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
            help.Copyright = "Author: Denis Volkov (user@domain.com)";
            help.Heading = "Tags Cloud Generator. Version: 0.01";
            return help;
        }
    }
}