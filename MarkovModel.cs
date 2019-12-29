using System;
using System.Collections.Generic;

namespace MarkovModelLib
{
    public class MarkovModel
    {
        public Dictionary<string, Dictogram> DictogramModel;
        public List<string> StartsList;

        public MarkovModel(string text)
        {
            DictogramModel = new Dictionary<string, Dictogram>();
            StartsList = new List<string>();
            MarkovModelMaker.UpdateMarkovModel(TextParser.ParseSentences(text), StartsList, DictogramModel);
        }

        public MarkovModel(Dictionary<string, Dictogram> dictogramModel, List<string> startsList)
        {
            DictogramModel = dictogramModel;
            StartsList = startsList;
        }

        public string Generate(int wordsCount)
        {
            return ModelIsNotNull() ? TextGenerator.GenerateMessage(DictogramModel, StartsList, wordsCount) : "я ничего не знаю, блин блять";
        }

        public string Generate(int wordsCount, string startWord)
        {
            return ModelIsNotNull() ? TextGenerator.GenerateMessage(DictogramModel, StartsList, wordsCount, startWord) : "я ничего не знаю, блин блять";
        }

        public void Update(string text)
        {
            MarkovModelMaker.UpdateMarkovModel(TextParser.ParseSentences(text), StartsList, DictogramModel);
        }

        public void Clear()
        {
            DictogramModel.Clear();
            StartsList.Clear();
        }

        public int Count()
        {
            var count = 0;
            foreach (var value in DictogramModel.Values)
            {
                count += value.Keys.Count;
            }

            return count;
        }

        public bool ModelIsNotNull()
        {
            var count = 0;
            foreach (var value in DictogramModel.Values)
            {
                count += value.Keys.Count;
                return count > 0;
            }

            return false;
        }
    }
}