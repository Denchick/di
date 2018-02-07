namespace TagsCloudContainer
{
    public interface IFileFormatReader
    {
        Result<string> Read();
    }
}