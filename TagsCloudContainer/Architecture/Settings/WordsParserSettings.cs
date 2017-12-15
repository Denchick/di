namespace TagsCloudContainer.Architecture
{
    public class WordsParserSettings
    {
        public int CountWordsToParse { get; }
        public WordsParserSettings(int wordsCount)
        {
            CountWordsToParse = wordsCount;
        }
    }
}