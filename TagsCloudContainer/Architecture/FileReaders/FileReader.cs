using System.IO;
using System.Windows.Forms.VisualStyles;
using TagsCloudContainer.Architecture;

namespace TagsCloudContainer
{
    public class FileReader : IFileFormatReader
    {
        private string Filename { get; set; }
        
        public FileReader(FileReaderSettings settings)
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