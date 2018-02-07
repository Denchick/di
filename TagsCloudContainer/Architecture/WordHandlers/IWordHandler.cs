namespace TagsCloudContainer.Architecture
{
    public interface IWordHandler
    {
        Result<string> Handle(string word);
    }
}