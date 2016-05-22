using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ReplaceCrlf
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length<1)
            {
                Console.WriteLine("参数错误!");
                Console.ReadKey();
                return;
            }
            for (int i = 0; i < args.Length; i++)
            {
                if (!File.Exists(args[i]))
                {
                    Console.WriteLine("文件不存在!");
                    Console.ReadKey();
                    return;
                }
            }
            for (int i = 0; i < args.Length; i++)
            {
                Console.WriteLine("Filename: " + args[i]);
                var newFilename = Path.GetFileName(args[i]) + " - replaced";
               var sr = new StreamReader(args[i], Encoding.UTF8);
                var sw = new StreamWriter(new FileStream(newFilename, FileMode.Create), Encoding.UTF8);
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    sw.Write(line + "\n");
                }
                sw.Flush();
                Console.WriteLine("Finished.");
                sr.Close();
                sw.Close();
                File.Delete(args[i]);
                File.Move(newFilename, args[i]);
            }
            Console.WriteLine("All finished.");
            Console.ReadKey();
        }
    }
}
