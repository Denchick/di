using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Mime;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Internal;
using TagsCloudContainer.Architecture;
using TagsCloudContainer.Utils;
using Moq;

namespace TagsCloudContainer.Tests
{
    public class TestSimpleWordsParser
    {
        private string Text { get; set; }
        
        private static SimpleWordsParser CreateSimpleWordsParser(string text, int wordsCountToParse)
        {
            var reader = new Mock<IFileFormatReader>();
            reader.Setup(r => r.Read()).Returns(text);
            var wordsParserSettings = new WordsParserSettings(wordsCountToParse);
            return new SimpleWordsParser(reader.Object, new NoHandler(), wordsParserSettings);
        }
        
        [Test]
        public void SimpleWordsParser_ParseCorrectly_WhenNothingToParse()
        {
            var simpleWordsParser = CreateSimpleWordsParser("", 100);
            simpleWordsParser.Parse().Should().BeEmpty();
        }

        [Test]
        public void SimpleWordsParser_ShouldCapitalizeFirstLetter_WhenOneWordInText()
        {
            var simpleWordsParser = CreateSimpleWordsParser("oneword", 100);

            var expected = new List<(string, int)> { ("Oneword", 1) };
            simpleWordsParser.Parse().Should().BeEquivalentTo(expected);
        }
        
        [Test]
        public void SimpleWordsParser_ShouldCountCorrectlyEqualsWords()
        {
            var simpleWordsParser = CreateSimpleWordsParser("oneword oneword", 100);

            var expected = new List<(string, int)> { ("Oneword", 2) };
            simpleWordsParser.Parse().Should().BeEquivalentTo(expected);
        }

        [TestCase("oneword oneword", 1, 1)]
        [TestCase("oneword anotherword", 2, 2)]
        [TestCase("oneword", 100, 1)]
        public void SimpleWordsParser_ShouldTakeExactlyCountWords(string text, int parameterCount, int expectedCount)
        {
            var simpleWordsParser = CreateSimpleWordsParser(text, parameterCount);

            simpleWordsParser.Parse().Should().HaveCount(expectedCount);
        }
        
        
        [TestCase("", true)]
        [TestCase(" ", true)]
        [TestCase("a", true)]
        [TestCase("ab", true)]
        [TestCase("abc", false)]
        [TestCase("and", true)]
        [TestCase("anderground)", false)]
        [TestCase("samarcand", false)]
        [TestCase("после", true)]
        public void SimpleWordsParser_IsBoring_WorksCorrectly(string word, bool expected)
        {
            SimpleWordsParser.IsBoring(word).Should().Be(expected);
        }
    }
}