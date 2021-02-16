
using FileWork;
using Processing;
using System;
using System.Collections.Generic;

using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Collections.Specialized;

namespace DBwork
{

    
    class DBworker : IDisposable
    {
        static string _Title = null;
        public static List<wordProcessing> Words = new List<wordProcessing>();
        public  DBworker save()
        {
            CreateTable();
            return null;
        }
        static void insert() 
        {
            using (SqlConnection connec = new SqlConnection(connect()))
            {
                connec.Open();
                try
                {
                    for (int i = 0; i < Words.Count; i++)
                    {
                        string query = $"INSERT INTO {_Title} (ID, word, wordQuanity) values ({i}, N'{Words[i]._word}', {Words[i]._quanity})";
                        SqlCommand command = new SqlCommand(query, connec);
                        command.ExecuteNonQuery();
                        command.Dispose();
                    }
                    connec.Close();
                    Console.WriteLine("Данные сохранены в БД  в таблицу с названием " + _Title);

                }
                catch (Exception insertExeptions) {
                    Console.WriteLine("Ошибка!");
                    FileWorker fileWorker = new FileWorker();
                    fileWorker.WriteExeptions("дата: " + Convert.ToString(DateTime.Now) + "метод insert" + insertExeptions.Message, ConfigurationManager.AppSettings.Get("DBworkerExeption"));
                    fileWorker.Dispose();
                }
            }
        }
        static string connect()
        {
            string connection = ConfigurationManager.AppSettings.Get("ConnectionString");
            return connection;
        }
        static void CreateTable()
        {
            Console.WriteLine("ВВедите название для таблицы в базе данных");
            string Title = Console.ReadLine();
            string query = $"CREATE TABLE {Title}" + "(ID int," + "word nvarchar(100),   wordQuanity INT)";
            using (SqlConnection newConnection = new SqlConnection(connect()))
            {
                try
                {
                    newConnection.Open();
                    SqlCommand sqlCom = new SqlCommand(query, newConnection);
                    sqlCom.ExecuteNonQuery();
                    newConnection.Close();
                    Console.WriteLine($"таблица  c названием {Title} создана");
                    _Title = Title;
                    insert();
                }
                catch (Exception createTableExeptions)
                {
                    Console.WriteLine("Таблица уже существует или название указанно не корректно");
                    using (FileWorker fileWorker = new FileWorker()) fileWorker.WriteExeptions("дата: " + Convert.ToString(DateTime.Now) + "метод createTable" + createTableExeptions.Message, ConfigurationManager.AppSettings.Get("DBworkerExeption"));
                    CreateTable();
                }
            }
        }
        public void Dispose()
        {
            GC.Collect();
        }



    }
}
            

            
        
    






