using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommandLine;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Architecture;
using TagsCloudContainer.Architecture.Tags;
using TagsCloudContainer.Architecture.TagsMakers;
using TagsCloudContainer.Architecture.Themes;

namespace TagsCloudContainer.Tests
{
    class ExampleLayouter:ICloudLayouter
    {
        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            return new Rectangle(new Point(0, 0), rectangleSize);
        }
    }

    class ExampleWordsParser : IWordsParser
    {
        private List<(string, int)> Parsed { get; }

        public ExampleWordsParser(string text)
        {
            Parsed = text
                .Split()
                .Where(e => !string.IsNullOrEmpty(e))
                .Select(e => (e, 1))
                .ToList();
        }
        public List<(string, int)> Parse()
        {
            return Parsed;
        }
    }
    
    public class TestTagsBuilder
    {
        private static TagsBuilder CreateTagsBuilder(string text)
        {
            var layouter = new ExampleLayouter();
            var settings = new ImageSettings("", 100, 100, new Stupid(), false);
            var wordsParser = new ExampleWordsParser(text);
            return new TagsBuilder(layouter, settings, wordsParser);
        }
        
        [Test]
        public void TagsBuilder_BuildCorrectly_WhenNoWords()
        {
            var builder = CreateTagsBuilder("");
            builder.Build().Should().BeNullOrEmpty();
        }

        [Test]
        public void TagsBuilder_BuildCorrectly_WhenOneWord()
        {
            var builder = CreateTagsBuilder("word!");
            builder.Build().Should().HaveCount(1);
        }
        
        [Test]
        public void TagsBuilder_BuildCorrectlyDifferentTags()
        {
            var builder = CreateTagsBuilder("word1, word2");
            builder.Build().Should().HaveCount(2);
        }
        
        [Test]
        public void TagsBuilder_BuiltShouldContainDifferentTagTypes()
        {
            var builder = CreateTagsBuilder("word1 word1 word1 word1 word2 word2 word2 word3 word3 word4");
            var types = builder.Build()
                .GroupBy(s => s.Type)
                .Select(g => g.Key)
                .ToList();
            
            var expectedTypes = new List<TagType>() { TagType.Biggest, TagType.Big, TagType.Medium, TagType.Small };
            types.ShouldAllBeEquivalentTo(expectedTypes);
        }
    }
}