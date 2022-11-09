using Newtonsoft.Json;
using System;
using System.Xml.Serialization;
using System.Xml;
using redactor;

internal class Program
    {
        public static List<slova> geometries = new List<slova>() { new slova("ЛМЗЦМЗТЦМШКМЫЛММЫЗТКШЫЖКЬМЫ", "9031142-0-", "32231120") };
        static void Main(string[] args)
        {
            Console.WriteLine("Введите файл который хотите открыть (.txt, .json, .xml)");
            Console.WriteLine("________________________________________________________");
            string savafile = Console.ReadLine();
            ToFile(savafile);
        }
        public static void ToFile(string readLine)
        {
            CreateFile.YUR = readLine;
            if (File.Exists(CreateFile.YUR))
            {
                Console.Clear();
                CreateFile.Open();
            }
            else
            {
                Console.WriteLine("ТАКОГО ФАЙЛА НЕТ ");
                if (CreateFile.Create())
                {
                    Console.WriteLine("НОВЫЙ ФАЙЛ УСПЕШНО СОЗДАН ");
                }
                else Console.WriteLine("ПОШЛО ЧТО НЕ ТАК ПОПРОБУЙТЕ ЗАНОВО СОЗДАТЬ ФАЙЛ  ");
                Close();
            }
        }
        public static void Close()
        {
            Environment.Exit(0);
        }
    }

