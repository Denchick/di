using System.Collections.Generic;
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

        public Result<string> Read()
        {
            if (!File.Exists(Filename))
                return Result.Fail<string>($"File is not found: {Filename}");
            var textFromFile = File.ReadAllText(Filename);
            return Result.Ok(textFromFile);
        }
    }
}