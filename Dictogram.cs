using System;
using System.Collections.Generic;
using System.Linq;

namespace MarkovModelLib
{
    public class Dictogram : Dictionary<string, int>
    {
        private readonly Random _random;
        public int KeysCount;
        public int TokensCount;

        public Dictogram()
        {
            KeysCount = 0;
            TokensCount = 0;
            _random = new Random();
        }
        
        public Dictogram(string[] iterable = null)
        {
            KeysCount = 0;
            TokensCount = 0;
            _random = new Random();
            if (iterable != null)
            {
                Update(iterable);
            }
        }

        public void Update(string[] iterable)
        {
            foreach (var word in iterable)
            {
                if (ContainsKey(word))
                {
                    this[word] += 1;
                    TokensCount += 1;
                }
                else
                {
                    this[word] = 1;
                    TokensCount += 1;
                    KeysCount += 1;
                }
            }
        }

        public int CountWord(string word)
        {
            if (ContainsKey(word))
            {
                return this[word];
            }

            return 0;
        }

        public string ReturnRandomWord()
        {
            return Keys.ToList()[_random.Next(0, Keys.ToList().Count - 1)];
        }

        public string ReturnWeightedRandomWord()
        {
            var randomIndex = _random.Next(0, TokensCount - 1);
            var index = 0;
            var keysList = Keys.ToList();
            for (var i = 0; i < KeysCount; i++)
            {
                index += this[keysList[i]];
                if (index >= randomIndex)
                {
                    index = i;
                    break;
                }
            }

            return keysList[index];
        }
    }
}
