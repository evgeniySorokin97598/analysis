using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Processing


/// <summary>
///     в переменной _htmlText записываются все слова из html страницы
///     в переменной _uniquenessWords записываются все уникальные слова 
/// 
/// </summary>
{
    class wordProcessing : IDisposable
    {
        public string _word;
        public int _quanity;
        static string _uniquenessWords = " ";
        public static string _htmlText = " ";
        public static void words(string text)
        { 
            Regex regex = new Regex(@"\w+", RegexOptions.IgnoreCase);
            MatchCollection matches = regex.Matches(text);
            for (int i = 0; i < matches.Count; i++) {
                _htmlText += matches[i].Value + ", ";  
                uniqueness(matches[i].Value); 
            }
        }
        static void uniqueness(string word) {
            
            Regex regex = new Regex(" " + word, RegexOptions.IgnoreCase);
            MatchCollection matches = regex.Matches(_uniquenessWords);
            if (matches.Count == 0) _uniquenessWords +=  " " + word + ",";  
        }
        public void counting (){
            Regex regex = new Regex(@"(\w+)", RegexOptions.IgnoreCase);
            MatchCollection matches = regex.Matches(_uniquenessWords); 
            for (int i = 0; i < matches.Count; i++) {
                Regex _regex = new Regex(" " + matches[i].Value + ",");
                MatchCollection _matches = _regex.Matches(_htmlText);
                Console.WriteLine("Слово : " + matches[i].Groups[1].Value + "          повторяется      " + _matches.Count + " раз (а)");
                DBwork.DBworker.Words.Add(new wordProcessing { _word = matches[i].Groups[1].Value, _quanity = _matches.Count });
            }
        }
        public void Dispose()
        {
            GC.Collect();
        }
    }
}
