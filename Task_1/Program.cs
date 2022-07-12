using System;
using System.IO;
using Task_1.BakeevoParse;

namespace Task_1
{
    internal class Program
    {
        readonly static string path = @"D:\Системные значки\Новая папка — копия\BakeevoPark.txt";
        readonly static string url = @"http://bakeevopark.ru/";


        static void Main(string[] args)
        {
            Start();
        }

        private static void Start()
        {
            ParserWorker<string[]> parser = new ParserWorker<string[]>(new BakeevoParser(), url);
            parser.OnCompleted += Parser_OnCompleted;
            parser.Worker();
            Console.ReadLine();
        }

        private static void Parser_OnCompleted(object arg1, string[] arg2)
        {
            WriteData(arg2);
            Console.WriteLine("Успешно");
        }

        private static void WriteData(string[] data)
        {
            StreamWriter sw = new StreamWriter(path, false);

            foreach (string item in data)
            {
                sw.WriteLine(item);
                //Console.WriteLine(item);
            }
            sw.Close();
        }
    }
}
