using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TagsCloudContainer.Architecture.Tags;
using TagsCloudContainer.Architecture.Themes;

namespace TagsCloudContainer.Architecture.TagsMakers
{
    public class StandardTagsBuilder : ITagsBuilder
    {
        private ITheme Theme { get; }
        private ICloudLayouter CloudLayouter { get; }
        private List<Tag> Tags { get; set; }

        public StandardTagsBuilder(ICloudLayouter cloudLayouter, ImageSettings settings, IWordsParser mostFrequentlyWords)
        {
            Theme = settings.Theme;
            CloudLayouter = cloudLayouter;
            Tags = MakeTagsFromTuples(mostFrequentlyWords.Parse())
                .ToList();
        }

        private static IEnumerable<Tag> MakeTagsFromTuples(List<(string, int)> wordAndFrequencyPairs)
        {
            var tags = new List<Tag>();
            var fifteenPercent = (int)(wordAndFrequencyPairs.Count * 0.15);
            var thirtyFivePercent = (int)(wordAndFrequencyPairs.Count * 0.35);

            tags.Add(new Tag(TagType.Biggest) { Text = wordAndFrequencyPairs.First().Item1 });

            tags.AddRange(wordAndFrequencyPairs
                .Skip(1)
                .Take(fifteenPercent)
                .Select(e => new Tag(TagType.Big) { Text = e.Item1 })
                .ToList());

            tags.AddRange(wordAndFrequencyPairs
                .Skip(1 + fifteenPercent)
                .Take(thirtyFivePercent)
                .Select(e => new Tag(TagType.Medium) { Text = e.Item1 })
                .ToList());

            tags.AddRange(wordAndFrequencyPairs
                .Skip(1 + fifteenPercent + thirtyFivePercent)
                .Select(e => new Tag(TagType.Small) { Text = e.Item1 })
                .ToList());

            return tags;
        }

        public IEnumerable<Tag> Build()
        {
            foreach (var tag in Tags)
            {
                var tagFont = Theme.GetTagAppearanceByType(tag.Type).Font;
                var tagSize = TextRenderer.MeasureText(tag.Text, tagFont);
                tag.Rectangle = CloudLayouter.PutNextRectangle(tagSize);
                yield return tag;
            }
        }
    }
}