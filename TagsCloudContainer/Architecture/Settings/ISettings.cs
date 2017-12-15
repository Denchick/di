namespace TagsCloudContainer.Architecture
{
    public interface ISettings
    {
        ImageSettings ImageSettings { get; set; }
        WordsParserSettings WordsParserSettings { get; set; }
        FileReaderSettings FileReaderSettings { get; set; }
    }
}