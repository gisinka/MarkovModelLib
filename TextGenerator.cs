using System;
using System.Collections.Generic;
using System.Linq;

namespace MarkovModelLib
{
    public static class TextGenerator
    {
        public static string GenerateMessage(Dictionary<string, Dictogram> markovModel, List<string> startsList,
            int wordsCount)
        {
            var currentWord = ChooseRandomStart(startsList);
            return GenerateText(markovModel, wordsCount, currentWord);
        }

        public static string GenerateMessage(Dictionary<string, Dictogram> markovModel, List<string> startsList,
            int wordsCount, string currentWord)
        {
            if (!startsList.Contains(currentWord))
            {
                currentWord = ChooseRandomStart(startsList);
            }

            return GenerateText(markovModel, wordsCount, currentWord);
        }

        private static string GenerateText(Dictionary<string, Dictogram> markovModel, int wordsCount, string currentWord)
        {
            var text = new List<string>();
            text.Add(currentWord);
            var random = new Random();
            for (var i = 1; i < wordsCount; i++)
            {
                var currentDictogram = markovModel.ContainsKey(currentWord)
                    ? markovModel[currentWord]
                    : markovModel.Values.ToList()[random.Next(0, markovModel.Values.Count - 1)];
                currentWord = currentDictogram.ReturnWeightedRandomWord();
                text.Add(currentWord);
            }

            return string.Join(' ', text) + '.';
        }

        private static string ChooseRandomStart(List<string> startsList)
        {
            var random = new Random();
            var start = startsList[random.Next(0, startsList.Count - 1)];
            return char.ToUpper(start[0])+start.Substring(1, start.Length - 1);
        }
    }
}
