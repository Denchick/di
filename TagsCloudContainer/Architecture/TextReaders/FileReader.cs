using System.IO;

namespace TagsCloudContainer
{
    public class FileReader : ITextReader
    {
        public string Read()
        {
            var textFromFile = File.ReadAllText(@"text.txt");
            return textFromFile;
        }
    }
}