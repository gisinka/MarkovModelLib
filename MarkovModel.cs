using System.Collections.Generic;

namespace MarkovModelLib
{
    public class MarkovModel
    {
        private readonly Dictionary<string, Dictogram> _markovModel;
        private readonly List<string> _startsList;

        public MarkovModel(string text)
        {
            _markovModel = new Dictionary<string, Dictogram>();
            _startsList = new List<string>();
            MarkovModelMaker.UpdateMarkovModel(TextParser.ParseSentences(text), _startsList, _markovModel);
        }

        public string Generate(int wordsCount)
        {
            return TextGenerator.GenerateMessage(_markovModel, _startsList, wordsCount);
        }

        public string Generate(int wordsCount, string startWord)
        {
            return TextGenerator.GenerateMessage(_markovModel, _startsList, wordsCount, startWord);
        }

        public void Update(string text)
        {
            MarkovModelMaker.UpdateMarkovModel(TextParser.ParseSentences(text), _startsList, _markovModel);
        }

        public void Clear()
        {
            _markovModel.Clear();
            _startsList.Clear();
        }

        public int Count()
        {
            var count = 0;
            foreach (var value in _markovModel.Values)
            {
                count += value.Keys.Count;
            }

            return count;
        }
    }
}