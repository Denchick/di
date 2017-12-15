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
        
        [Option('h', "height", Required = false, DefaultValue = 600,
            HelpText = "Высота изображения в пискелах")]
        public int Height { get; set; }
        
        [Option('w', "width", Required = false, DefaultValue = 600,
            HelpText = "Ширина изображения в пискелах")]
        public int Width { get; set; }
        
        [Option('e', "theme", Required = false, DefaultValue = "NightMode",
            HelpText = "Использовать определенную тему. Пока есть 2: NightMode и Stupid")]
        public string Theme { get; set; }
        
        
        
        [Option('c', "count", DefaultValue = 70, HelpText = "Количество слов из текста в облаке")]
        public int Count { get; set; }
        
        
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