using System;
using CommandLine;
using TagsCloudContainer.Architecture.Themes;

namespace TagsCloudContainer.Architecture
{
    public class AppSettings : ISettings
    {
        public ImageSettings ImageSettings { get; set; }
        public WordsParserSettings WordsParserSettings { get; set; }
        public FileReaderSettings FileReaderSettings { get; set; }

        public AppSettings(ImageSettings imageSettings, WordsParserSettings wordsParserSettings,
            FileReaderSettings fileReaderSettings)
        {
            ImageSettings = imageSettings;
            WordsParserSettings = wordsParserSettings;
            FileReaderSettings = fileReaderSettings;
        }

        public static AppSettings FromArgs(string[] args)
        {
            var options = new Options();
            if (!Parser.Default.ParseArguments(args, options))
            {
                options.GetUsage();
                throw new Exception("Start program without any arguments");
            }

            var imageSettings = new ImageSettings(
                options.ImageFilename, options.Height, options.Width, GetThemeByName(options.Theme), options.Gui);
            var wordsParserSettings = new WordsParserSettings(options.Count);
            var fileReaderSettings = new FileReaderSettings(options.InputFileName);
            
            return new AppSettings(imageSettings, wordsParserSettings, fileReaderSettings);
        }
        
        private static ITheme GetThemeByName(string themeName)
        {
            if (themeName == "NightMode")
                return new NightMode();
            return new Stupid();
        }
    }
    

}