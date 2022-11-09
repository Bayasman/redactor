using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace redactor
{
    internal class CreateFile
    {
        public static string YUR;
        public static void Open()
        {
            Console.WriteLine("СОХРАНИТЬ ФАЙЛ В ОДНОМ ИЗ ТРЕХ ФРАГМЕНТОВ (txt, json, xml) - F1.  Escape - ЗАКРЫТЬ ПРОГРАММУ");
            Console.WriteLine("_______________________________________________________________________________________________");
            string extension = Path.GetExtension(YUR);
            if (extension == ".txt")
            {
                string[] list = File.ReadAllLines(YUR);
                foreach (var item in list) Console.WriteLine(item);
            }

            else if (extension == ".json")
            {
                string text = File.ReadAllText(YUR);
                List<slova> geometries = JsonConvert.DeserializeObject<List<slova>>(text);
                foreach (slova item in geometries)
                {
                    Console.WriteLine(item.slovo);
                    Console.WriteLine(item.Sentencelength.ToString());
                    Console.WriteLine(item.width.ToString());
                }
            }
            else if (extension == ".xml")
            {
                XmlSerializer xml = new XmlSerializer(Program.geometries.GetType());
                using (FileStream fs = new FileStream(YUR, FileMode.OpenOrCreate))
                {
                    List<slova> list = (List<slova>)xml.Deserialize(fs);
                    foreach (slova item in list)
                    {
                        Console.WriteLine(item.slovo);
                        Console.WriteLine(item.Sentencelength.ToString());
                        Console.WriteLine(item.width.ToString());
                    }
                }
            }
            Button();
        }
        public static bool Create()
        {
            string extension = Path.GetExtension(YUR);
            if (extension == ".txt")
            {
                using StreamWriter sw = new StreamWriter(YUR);
                for (int i = 0; i < Program.geometries.Count; i++)
                {
                    sw.WriteLine(Program.geometries[i].slovo);
                    sw.WriteLine(Program.geometries[i].Sentencelength.ToString());
                    sw.WriteLine(Program.geometries[i].width.ToString());
                }
            }
            else if (extension == ".json")
            {
                using StreamWriter sw = File.CreateText(YUR); for (int i = 0; i < Program.geometries.Count; i++)
                {
                    sw.WriteLine(Program.geometries[i].slovo);
                    sw.WriteLine(Program.geometries[i].Sentencelength.ToString());
                    sw.WriteLine(Program.geometries[i].width.ToString());
                }
                sw.WriteLine(JsonConvert.SerializeObject(Program.geometries));
            }
            else if (extension == ".xml")
            {
                XmlSerializer xml = new XmlSerializer(Program.geometries.GetType());
                using FileStream fs = new FileStream(YUR, FileMode.OpenOrCreate);
                xml.Serialize(fs, Program.geometries);
            }
            else return false;
            return true;
        }

        private static void Save()
        {
            Console.Clear();
            Console.WriteLine("ВвЕДИТЕ ПУТЬ КУДА СОХРАНИТЕ ФАЙЛ");
            Console.WriteLine("__________________________________________________");
            string readLine = Console.ReadLine();
            CreateFile.YUR = readLine;
            if (CreateFile.Create())
            {
                Console.WriteLine("ВСЕ СОХРАНИЛОСЬ  ! ПРИХОДИТЕ ЕЩЕ ! ");
            }
            else Console.WriteLine("ЧТО ТО ПОШЛО НЕ ТАК! ПОБРУЙ ЗАНОВО ПО БРАТСКИ ");
            Program.Close();
        }
        private static void Button()
        {
            ConsoleKeyInfo button = Console.ReadKey();
            if (button.Key == ConsoleKey.Escape) Program.Close();
            else if (button.Key == ConsoleKey.F1) CreateFile.Save();
            else
            {
                Console.Clear();
                Program.ToFile(CreateFile.YUR);
            }
        }
    }
}
