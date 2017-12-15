namespace TagsCloudContainer.Architecture
{
    public class FileReaderSettings
    {
        public string Filename { get; }
        
        public FileReaderSettings(string inputFileName)
        {
            Filename = inputFileName;
        }
    }
}