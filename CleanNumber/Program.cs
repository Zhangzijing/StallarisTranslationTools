using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CleanNumber
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("-----------CleanNumber1.1-------------");
            Console.WriteLine("本程序清除定位ID\n" +
                              "    发现bug请直接反馈给void QQ:296263103.\n");
            Console.WriteLine("请在最后关头使用本软件, 别用来处理没有ID的文件");
            Console.WriteLine("-------------------------------------");
            Console.ReadKey();
            #region c
            var locDir = Environment.CurrentDirectory + "\\localisation";
            var fileList = new List<string>
            {
                "dip_messages_l_english.yml",
                "events_2_l_english.yml",
                "events_3_l_english.yml",
                "events_4_l_english.yml",
                "events_l_english.yml",
                "event_chains_l_english.yml",
                "l_english.yml",
                "mandates_l_english.yml",
                "messages_l_english.yml",
                "modifiers_2_l_english.yml",
                "modifiers_3_l_english.yml",
                "modifiers_l_english.yml",
                "name_lists_l_english.yml",
                "pop_factions_l_english.yml",
                "prescripted_l_english.yml",
                "projects_2_l_english.yml",
                "projects_3_l_english.yml",
                "projects_4_l_english.yml",
                "projects_l_english.yml",
                "ship_sections_l_english.yml",
                "standalone_l_english.yml",
                "technology_l_english.yml",
                "triggers_effects_l_english.yml",
                "tutorial_l_english.yml"
            };
            if (!Directory.Exists(locDir))
            {
                Console.WriteLine("未找到{0}, \n请把本程序放在localisation的上一级目录!", locDir);
                Console.ReadKey();
                return;
            }
            bool checkPassed = true;
            foreach (var checkingFile in fileList)
            {
                if (!File.Exists(locDir + "\\" + checkingFile))
                {
                    checkPassed = false;
                    Console.WriteLine("缺失文件:" + checkingFile);
                }
            }
            if (!checkPassed)
            {
                Console.ReadKey();
                return;
            }
            #endregion
            var indexedPath = Environment.CurrentDirectory + "\\deindexed\\";
            if (!Directory.Exists(indexedPath)) Directory.CreateDirectory(indexedPath);
            else foreach (var file in new DirectoryInfo(indexedPath).GetFiles()) file.Delete();
            foreach (var checkingFile in fileList)
            {
                Console.Write("处理文件 " + checkingFile + "...");
                var fileName = locDir + "\\" + checkingFile;
                try
                {
                    var fs = File.Open(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                    var sr = new StreamReader(fs, Encoding.UTF8);


                    var sw = new StreamWriter(new FileStream(indexedPath + checkingFile, FileMode.Create), Encoding.UTF8);
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (!line.StartsWith("#") && line.Contains(":") && line.EndsWith("\""))
                        {
                            var cl = CleanID(line);
                            if (cl==null)
                            {
                                Console.WriteLine("文件损坏!");
                                Console.ReadKey();
                                return;
                            }
                            sw.WriteLine(cl);
                        }
                        else
                            sw.Write(line + "\n");
                        sw.Flush();
                    }
                    sw.Flush();
                }
                catch (Exception e)
                {
                    // Let the user know what went wrong.
                    Console.WriteLine("文件读取失败:");
                    Console.WriteLine(e.Message);
                    return;
                }
                Console.WriteLine("完毕.");
            }
            Console.WriteLine("所有文件处理完毕, 结果在deindexed目录, 现在, 可以退出了.");
            Console.ReadKey();
        }

        private static string CleanID(string s)
        {
            int pos = s.Length-2;
            var chars = s.ToCharArray();
            while (chars[pos] != ',')
            {
                pos--;
                if (s.Length - pos>7)
                {
                    
                    return null;
                }
            }
            return s.Substring(0, pos) + "\"";
        }
    }
}
