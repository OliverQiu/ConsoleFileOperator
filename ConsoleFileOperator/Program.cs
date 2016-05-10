using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleFileOperator
{
    class Program
    {
        static void Main(string[] args)
        {
            FileUtil util = new FileUtil();
            util.StartService(true);
            //foreach(var item in Directory.GetFiles("C:\\Program Files")){
            //    string loc = "C:\\Program Files";
            //    string logpath = Path.Combine(loc,"log");
            //    Console.WriteLine(item.Split('\\').LastOrDefault());
            //    Console.WriteLine(logpath);
            //}
            Console.Read();
        }
    }
}
