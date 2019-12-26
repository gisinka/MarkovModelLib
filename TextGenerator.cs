using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarkovModelLib
{
    internal static class TextGenerator
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
                currentWord = ChooseRandomStart(startsList);
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
                var randomWeightedWord = currentDictogram.ReturnWeightedRandomWord();
                currentWord = randomWeightedWord;
                text.Add(currentWord);
            }

            return string.Join(' ', text) + '.';
        }

        private static string ChooseRandomStart(List<string> startsList)
        {
            var random = new Random();
            return startsList[random.Next(0, startsList.Count - 1)];
        }
    }
}
