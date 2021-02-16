using Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace FileWork

/// <summary>
///     метод WriteExeptions записывает исключения в файл с логами
///     метод pattern извлекает текст из html элементов 
///     метод ClearHtml удаляет из текста блоки со скриптами и стилями
/// </summary>
{
    class FileWorker : IDisposable
    {
        public void WriteExeptions(string exeptionsText, string path)
        {
            using (FileStream FS = new FileStream(path, FileMode.Append))
            {
                using (StreamWriter SW = new StreamWriter(FS))
                {
                    SW.WriteLine(exeptionsText);
                }
            }
        }
        public static void Read(string path)
        {
                using (FileStream FS = new FileStream(path, FileMode.Open))
                {
                    using (StreamReader SS = new StreamReader(FS))
                    {
                        Pattern(SS.ReadToEnd());
                    }
                }
        }
        static void Pattern(string sourse) // ивзлекает текст из html элементов 
        {
            Regex regex = new Regex(@"([<]\s{0,}?\w.{0,}?>)+([\D]{0,1}?)(\s{0,}?\w.{0,}?)?(\W{0,1})(<.{0,}?\W(\s{0,}?)\w.{0,}?>)");
            sourse = ClearHtml(sourse);
            MatchCollection matches = regex.Matches(sourse);
            for (int i = 0; i < matches.Count; i++)
            {
                if (matches[i].Groups[3].Value != "")
                {
                    wordProcessing.words(matches[i].Groups[3].Value);
                }
            }
        }
        static string ClearHtml(string sourse) // удаляет стили и скриты 
        {
            Regex deleteSripts = new Regex(@"<script.*>");
            sourse = deleteSripts.Replace(sourse, " ");
            Regex deleteStyles = new Regex(@"<style.*>");
            sourse = deleteStyles.Replace(sourse, "  ");
            return sourse;
        }
        public void Dispose()
        {
            GC.Collect();
        }
    }
}
