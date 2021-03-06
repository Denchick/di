﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using TagsCloudContainer.Architecture;
using TagsCloudContainer.Utils;

namespace TagsCloudContainer
{
    public class SimpleWordsParser : IWordsParser
    {
        private IFileFormatReader  Reader { get; set; }
        private int CountWordsToParse { get; set; }
        private IWordHandler WordHandler { get; set; }

        public SimpleWordsParser(IFileFormatReader reader, IWordHandler wordHandler, WordsParserSettings settings)
        {
            Reader = reader;
            CountWordsToParse = settings.CountWordsToParse;
            WordHandler = wordHandler;
        }
        private static readonly HashSet<string> BoringWords = new HashSet<string>()
        {
            "aboard", "about", "above", "across", "after", "against", "along", "amid", "among", "anti",
            "around", "as", "at", "before", "behind", "below", "beneath", "beside", "besides", "between",
            "beyond", "but", "by", "concerning", "considering", "despite", "down", "during", "except`",
            "excepting", "excluding", "following", "for", "from", "in", "inside", "into", "like", "minus",
            "near", "of", "off", "on", "onto", "opposite", "outside", "over", "past", "per", "plus", "and",
            "regarding", "round", "save", "since", "than", "through", "to", "toward", "towards", "under",
            "underneath", "unlike", "until", "up", "upon", "versus", "via", "with", "within", "without",

            "в", "с", "у", "о", "к", "от", "до", "на", "по", "со", "из", "над", "под", "при", "про", "без",
            "ради", "близ", "перед", "около", "через", "вдоль", "после", "кроме", "сквозь", "вроде",
            "вследствие", "благодаря", "вопреки", "согласно", "навстречу", "всем",
            "него", "меня", "себя", "тебя", "мой", "твой", "свой", "ваш", "наш", "его", "её", "их",
            "кто", "что", "какой", "чей", "где", "который", "откуда", "сколько", "каковой", "каков",
            "зачем", "тот", "этот", "столько", "такой", "таков", "сей", "всякий", "каждый", "сам",
            "иной", "другой", "весь", "некто", "нечто", "некоторый", "несколько", "кто-то", "что-нибудь",
            "какой-либо", "и", "не","на", "из","в","с","о","за","от","что",
            "так", "как",
        };

        private List<(string, int)> GetMostFrequentWords(string text, int count)
        {
            return Regex.Split(text.ToLower(), @"\W+")
                .Select(e => WordHandler.Handle(e))
                .Where(word => !string.IsNullOrWhiteSpace(word) && !IsBoring(word))
                .GroupBy(word => word)
                .Select(group => (group.Key.CapitalizeFirstLetter(), group.Count()))
                .OrderByDescending(tuple => tuple.Item2)
                .ThenBy(tuple => tuple.Item1)
                .Take(count)
                .ToList();
        }

        public static bool IsBoring(string word)
        {
            return BoringWords.Contains(word.ToLower()) || word.Length <= 2;
        }
        
        public List<(string, int)> Parse()
        {
            var text = Reader.Read();
            if (string.IsNullOrEmpty(text)) return new List<(string, int)>();
            var frequentWords = GetMostFrequentWords(text, CountWordsToParse);
            return frequentWords;
        }
    }
}