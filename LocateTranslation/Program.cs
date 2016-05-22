using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.IO;
namespace LocateTranslation
{
    class Program
    {
        static void Main(string[] args)
        {
            var notepadppPath = Environment.CurrentDirectory + "\\notepad++.txt";
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("本程序可以快速定位指定ID的翻译所在的位置\n" +
                              "    发现bug请直接反馈给void QQ:296263103.\n");
            Console.WriteLine("如果你安装了notepad++, 请在本程序所在目录下\n" +
                              "建立notepad++.txt文件, 写上notepad++.exe的" +
                              "路径, 即可自动定位到行");
            Console.WriteLine("-------------------------------------");

            
            string notepadpp  = null;
            if (File.Exists(notepadppPath))
            {
                string con = File.ReadAllText(notepadppPath);
                if (File.Exists(con))
                {
                    notepadpp = con;
                    Console.WriteLine("Notepad++路径设置成功!");
                }
            }
            var mappingpath = Environment.CurrentDirectory + "\\Mapping";
            if (!File.Exists(mappingpath))
            {
                Console.WriteLine("Mapping文件未找到, 请把它放在本程序同级目录下.");
                Console.ReadKey();
                return;
            }
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

            string[] lines = File.ReadAllLines(mappingpath);
            while (true)
            {
                Console.WriteLine("请输入翻译ID(如2333):");
                var idStr = Console.ReadLine();
                try
                {
                    int id = int.Parse(idStr);
                    var spl = lines[id].Split(',');
                    Console.WriteLine("找到: " + id + "号翻译, 位于文件" + spl[1] + "的第"+ spl[2] + "行");
                    if (notepadpp==null)
                    {
                        Process.Start("notepad",locDir + "\\" + spl[1]);
                    }
                    else
                    {
                        Process.Start(notepadpp, "\"" +  locDir + "\\" + spl[1] + "\" -n" + spl[2]);
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("发生错误!");
                }
                Console.WriteLine();
            }
            
        }
    }
}
