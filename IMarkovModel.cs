using System.Collections.Generic;

namespace MarkovModelLib
{
    public interface IMarkovModel
    {
        Dictionary<string, Dictogram> DictogramModel { get; set; }
        List<string> Starts { get; set; }
        public string Generate(int wordsCount);
        public string Generate(int wordsCount, string startWord);
        public void Update(string text);
        public void Clear();
        public int Count();
        bool ModelIsNotNull();
    }
}