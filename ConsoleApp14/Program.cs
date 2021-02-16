using System;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;

using HTMLwork;

namespace ConsoleApp13
{
    class Program
    {
        
        
        static void Main(string[] args)
        {
            Console.WriteLine("ВВедите url адрес");
            string url = Console.ReadLine();
            HtmlWorker download = new HtmlWorker(url) ;
            download.Dispose();
        }
    }
}
