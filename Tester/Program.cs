using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileName = "dip_messages_l_english.yml";
            var fs = File.Open(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
            var sr = new StreamReader(fs);
            var indexedPath = Environment.CurrentDirectory + "\\indexed\\" ;
            if (!Directory.Exists(indexedPath))
            {
                Directory.CreateDirectory(indexedPath);
            }
            var sw = new StreamWriter(new FileStream(indexedPath + fileName, FileMode.Create));
            string line;
            sw.WriteLine("l_english:\n");
            while ((line = sr.ReadLine()) != null)
            {
                
                if (!line.StartsWith("#") && line.Contains(":") && line.EndsWith("\""))
                {
                   
                    sw.WriteLine(line.Substring(0,line.Length-1) + "[0]\"");
                }
            }
            sw.Flush();
        }
    }
}
