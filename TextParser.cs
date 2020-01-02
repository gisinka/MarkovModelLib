using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MarkovModelLib
{
    internal static class TextParser
    {
        public static List<List<string>> ParseSentences(string text)
        {
            var sentencesList = new List<List<string>>();
            var textBuilder = new StringBuilder(text);
            if (!(string.IsNullOrEmpty(text) || char.IsPunctuation(text[text.Length - 1])))
                textBuilder.Append(".");
            if (!(string.IsNullOrEmpty(text) || char.IsUpper(text[0])))
                textBuilder[0] = char.ToUpper(textBuilder[0]);
            foreach (Match sentence in Regex.Matches(textBuilder.ToString(), @"[А-ЯЁ][\S\s]+?(?:[\S][^А-ЯЁ\.]){1,}(?:\.+|[?!])(?!(\s*[а-яё)\-""«0-9\.]))", RegexOptions.Multiline))
            {
                var sentenceDivided = DivideWords(sentence.Value);
                if (sentenceDivided.Count > 0)
                {
                    sentencesList.Add(sentenceDivided);
                }
            }

            return sentencesList;
        }

        private static List<string> DivideWords(string sentence)
        {
            var wordsList = new List<string>();
            var word = new StringBuilder();
            for (var i = 0; i < sentence.Length; i++)
            {
                if (sentence[i] == '\'' || char.IsLetter(sentence[i]) || (i > 0 && sentence[i] == ',' && !(char.IsPunctuation(sentence[i - 1]) || sentence[i-1] == ' ')))
                {
                    word.Append(sentence[i]);
                }
                else if (word.Length > 0)
                {
                    wordsList.Add(Regex.Replace(word.ToString().Trim(), "[ ]+", " "));
                    word.Clear();
                }
            }

            if (word.Length > 0)
            {
                wordsList.Add(word.ToString());
            }

            return wordsList;
        }
    }
}
