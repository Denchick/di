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
    }
    

}