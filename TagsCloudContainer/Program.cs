using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autofac;
using TagsCloudContainer.Architecture;
using TagsCloudContainer.Utils;

namespace TagsCloudContainer
{
    class Program
    {
        private static readonly HashSet<string> boringWords = new HashSet<string>()
        {
            "aboard", "about", "above", "across", "after", "against", "along", "amid", "among", "anti",
            "around", "as", "at", "before", "behind", "below", "beneath", "beside", "besides", "between",
            "beyond", "but", "by", "concerning", "considering", "despite", "down", "during", "except`",
            "excepting", "excluding", "following", "for", "from", "in", "inside", "into", "like", "minus",
            "near", "of", "off", "on", "onto", "opposite", "outside", "over", "past", "per", "plus",
            "regarding", "round", "save", "since", "than", "through", "to", "toward", "towards", "under",
            "underneath", "unlike", "until", "up", "upon", "versus", "via", "with", "within", "without",

            "в", "с", "у", "о", "к", "от", "до", "на", "по", "со", "из", "над", "под", "при", "про", "без",
            "ради", "близ", "перед", "около", "через", "вдоль", "после", "кроме", "сквозь", "вроде",
            "вследствие", "благодаря", "вопреки", "согласно", "навстречу",
            "него", "меня", "себя", "тебя", "мой", "твой", "свой", "ваш", "наш", "его", "её", "их",
            "кто", "что", "какой", "чей", "где", "который", "откуда", "сколько", "каковой", "каков",
            "зачем", "тот", "этот", "столько", "такой", "таков", "сей", "всякий", "каждый", "сам",
            "иной", "другой", "весь", "некто", "нечто", "некоторый", "несколько", "кто-то", "что-нибудь",
            "какой-либо", "и", "не","на", "из","в","с","о","за","от","что",
            "так", "как",
        };

        public static void Main()
        {
            var builder = new ContainerBuilder();


            var textFromFile = File.ReadAllText(@"text.txt");
            var mostFrequentWords = GetMostFrequentWords(textFromFile, 70);

            var cloudCenter = new Point(300, 300);
            var layouter = new CircularCloudLayouter(cloudCenter);

            var tags = layouter.MakeTagsFromTuples(mostFrequentWords);
            layouter.SetRectangeForEachTag(tags);

            var tagsDrawer = new TagsDrawer("image.bmp", tags);
        }

        public static List<(string, int)> GetMostFrequentWords(string text, int count)
        {
            return Regex.Split(text.ToLower(), @"\W+")
                .Where(word => !string.IsNullOrWhiteSpace(word) && !IsBoring(word))
                .GroupBy(word => word)
                .Select(group => (group.Key.CapitalizeFirstLetter(), group.Count()))
                .OrderByDescending(tuple => tuple.Item2)
                .ThenBy(tuple => tuple.Item1)
                .Take(count)
                .ToList();
        }

        private static bool IsBoring(string word)
        {
            return boringWords.Contains(word.ToLower()) && word.Length <= 2;
        }
    }
}
