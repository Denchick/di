using System.IO;
using System.Windows.Forms.VisualStyles;

namespace TagsCloudContainer
{
    public class FileReader : ITextReader
    {
        public string Filename { get; set; }
        
        public FileReader(string filename)
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