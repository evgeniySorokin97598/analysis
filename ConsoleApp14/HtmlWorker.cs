using DBwork;
using FileWork;
using Processing;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace HTMLwork
{
    class HtmlWorker:IDisposable
    {
        static string _url;
        public HtmlWorker(string url)
        {
            _url = url;
            Download();
            Dispose();
            wordProcessing wordProcessing = new wordProcessing();
            wordProcessing.counting();
            wordProcessing.Dispose();
            DBworker DB = new DBworker();
            DB.save();
            DB.Dispose();
        }
        static void Download() 
        {
            string path = ConfigurationManager.AppSettings.Get("DownloadHtmlPath") + FileName() + ".html";
            try
            {
                using (WebClient client = new WebClient()) client.DownloadFile(_url, path);
                FileWorker.Read(path);
            }

            catch (Exception downloadExepions) {
                FileWorker fileWorker = new FileWorker();
                fileWorker.WriteExeptions("дата:" + Convert.ToString(DateTime.Now) + "|| метод Download: " + downloadExepions.Message, ConfigurationManager.AppSettings.Get("DownloadHtmlExeptions"));
                fileWorker.Dispose();
                Console.WriteLine("Не корректная ссылка, введите повторно");
                _url = Console.ReadLine();
                Download();
            }
        }
        static string FileName()
        {
            Console.WriteLine("ВВедите название для html файла. \nТолько буквы и (или) цифры");
            string Name = Console.ReadLine();
                Regex regex = new Regex("\\W") ;
                MatchCollection matches = regex.Matches(Name);
                if (matches.Count > 0) {
                FileWorker fileWorker = new FileWorker();
                Console.WriteLine("Имя не корректно");
                    FileName();
                }
            return Name;
        }
        public void Dispose()
        {
            GC.Collect();
        }
    }


    
}
