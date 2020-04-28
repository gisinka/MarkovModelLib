using System;
using System.Collections.Generic;
using System.Linq;

namespace MarkovModelLib
{
    public class Dictogram : Dictionary<string, int>
    {
        private readonly Random _random;

        public Dictogram()
        {
            _random = new Random();
        }
        
        public Dictogram(string[] iterable = null)
        {
            _random = new Random();
            if (iterable != null)
            {
                Update(iterable);
            }
        }

        public void Update(IEnumerable<string> iterable)
        {
            foreach (var word in iterable)
            {
                if (ContainsKey(word))
                {
                    this[word] += 1;
                    
                }
                else
                {
                    this[word] = 1;
                }
            }
        }

        public string ReturnRandomWord()
        {
            return Keys.ToList()[_random.Next(0, Keys.ToList().Count - 1)];
        }

        public string ReturnWeightedRandomWord()
        {
            var randomIndex = _random.Next(0, this.Values.Count - 1);
            var index = 0;
            var keysList = Keys.ToList();
            for (var i = 0; i < this.Keys.Count; i++)
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
