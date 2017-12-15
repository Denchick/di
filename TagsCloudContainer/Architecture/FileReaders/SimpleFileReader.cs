using System.IO;
using System.Windows.Forms.VisualStyles;
using TagsCloudContainer.Architecture;

namespace TagsCloudContainer
{
    public class SimpleFileReader : IFileFormatReader
    {
        private string Filename { get; set; }
        
        public SimpleFileReader(ISettings settings)
        {
            Filename = settings.FileReaderSettings.Filename;
        }

        public string Read()
        {
            var textFromFile = File.ReadAllText(Filename);
            return textFromFile;
        }
    }
}