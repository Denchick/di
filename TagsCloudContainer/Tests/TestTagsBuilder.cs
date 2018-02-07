using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommandLine;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using TagsCloudContainer.Architecture;
using TagsCloudContainer.Architecture.Tags;
using TagsCloudContainer.Architecture.TagsMakers;
using TagsCloudContainer.Architecture.Themes;

namespace TagsCloudContainer.Tests
{    
    public class TestTagsBuilder
    {
        private static TagsBuilder CreateTagsBuilder(string text)
        {
            var layouter = new Mock<ICloudLayouter>();
            layouter.Setup(l => l.PutNextRectangle(It.IsAny<Size>()))
                .Returns((Size size) => new Rectangle(new Point(0, 0), size));
            
            var settings = new ImageSettings("", 100, 100, new Stupid(), false);
            
            var returnedText = text
                .Split()
                .Where(e => !string.IsNullOrEmpty(e))
                .Select(e => (e, 1))
                .ToList();
            var wordsParser = new Mock<IWordsParser>();
            wordsParser.Setup(l => l.Parse()).Returns(returnedText);
            return new TagsBuilder(layouter.Object, settings, wordsParser.Object);
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
                .GroupBy(s => s.Value.Type)
                .Select(g => g.Key)
                .ToList();
            
            var expectedTypes = new List<TagType>() { TagType.Biggest, TagType.Big, TagType.Medium, TagType.Small };
            types.ShouldAllBeEquivalentTo(expectedTypes);
        }
    }
}