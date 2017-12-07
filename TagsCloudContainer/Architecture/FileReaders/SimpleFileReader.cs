using System.IO;
using System.Windows.Forms.VisualStyles;

namespace TagsCloudContainer
{
    public class SimpleFileReader : IFileFormatReader
    {
        public string Filename { get; set; }
        
        public SimpleFileReader(string filename)
        {
            Filename = filename;
        }

        public string Read()
        {
            var textFromFile = File.ReadAllText(Filename);
            return textFromFile;
        }
    }
}