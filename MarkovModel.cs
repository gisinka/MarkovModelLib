using System.Collections.Generic;
using System.Linq;

namespace MarkovModelLib
{
    public class MarkovModel : IMarkovModel
    {
        public Dictionary<string, Dictogram> DictogramModel { get; set; }
        public List<string> Starts { get; set; }

        public MarkovModel()
        {
            DictogramModel = new Dictionary<string, Dictogram>();
            Starts = new List<string>();
        }

        public MarkovModel(string text) : this()
        {
            MarkovModelMaker.UpdateMarkovModel(TextParser.ParseSentences(text), Starts, DictogramModel);
        }

        public MarkovModel(List<string> starts, Dictionary<string, Dictogram> dictogramModel)
        {
            Starts = starts;
            DictogramModel = dictogramModel;
        }

        public MarkovModel(Dictionary<string, Dictogram> dictogramModel, List<string> starts) : this(starts,
            dictogramModel)
        {
        }

        public string Generate(int wordsCount)
        {
            return ModelIsNotNull()
                ? TextGenerator.GenerateMessage(DictogramModel, Starts, wordsCount)
                : "я ничего не знаю, блин";
        }

        public string Generate(int wordsCount, string startWord)
        {
            return ModelIsNotNull()
                ? TextGenerator.GenerateMessage(DictogramModel, Starts, wordsCount, startWord)
                : "я ничего не знаю, блин";
        }

        public void Update(string text)
        {
            MarkovModelMaker.UpdateMarkovModel(TextParser.ParseSentences(text), Starts, DictogramModel);
        }

        public void Clear()
        {
            DictogramModel.Clear();
            Starts.Clear();
        }

        public int Count()
        {
            return DictogramModel.Values.Sum(value => value.Keys.Count);
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