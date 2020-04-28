using System.Collections.Generic;
using System.Text;

namespace MarkovModelLib
{
    public static class MarkovModelMaker
    {
        public static void UpdateMarkovModel(IEnumerable<List<string>> text, List<string> startsList, Dictionary<string, Dictogram> markovModel)
        {
            foreach (var sentence in text)
            {
                CreateNGramKeys(sentence, markovModel, 2, startsList);
                CreateNGramKeys(sentence, markovModel, 3, startsList);
            }
        }

        private static void CreateNGramKeys(IReadOnlyList<string> sentence, IDictionary<string, Dictogram> markovModel,
            int gramDimension, ICollection<string> startsList)
        {
            var firstKey = new StringBuilder();
            var secondKey = new StringBuilder();
            for (var i = 0; i < sentence.Count - gramDimension + 1; i++)
            {
                for (var m = 0; m < gramDimension - 1; m++)
                {
                    firstKey.Append(sentence[m + i] + " ");
                    if (i == 0 && m == gramDimension - 2)
                    {
                        var start = firstKey.ToString().Substring(0, firstKey.Length - 1);
                        if (char.IsUpper(start[0]) && char.IsLower(start[^1]))
                        {
                            start = start.ToLowerInvariant();
                            firstKey = new StringBuilder(start);
                        }

                        startsList.Add(start);
                    }
                    if (m == gramDimension - 2)
                    {
                        secondKey.Append(sentence[m + i + 1]);
                    }
                }
                
                firstKey.Remove(firstKey.Length - 1, 1);
                markovModel.AddNGram(firstKey.ToString(), secondKey.ToString());
                firstKey.Clear();
                secondKey.Clear();
            }
        }

        private static void AddNGram(this IDictionary<string, Dictogram> markovModel, string firstKey, string secondKey)
        {
            if (markovModel.ContainsKey(firstKey))
            {
                markovModel[firstKey].Update(new[] { secondKey });
            }
            else
            {
                markovModel[firstKey] = new Dictogram(new[] { secondKey });
            }
        }
    }
}
