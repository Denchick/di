using System.IO;
using System.Windows.Forms.VisualStyles;
using TagsCloudContainer.Architecture;

namespace TagsCloudContainer
{
    public class SimpleFileReader : IFileFormatReader
    {
        private string Filename { get; set; }
        
        public SimpleFileReader(FileReaderSettings settings)
        {
            Filename = settings.Filename;
        }

        public string Read()
        {
            var textFromFile = File.ReadAllText(Filename);
            return textFromFile;
        }
    }
}