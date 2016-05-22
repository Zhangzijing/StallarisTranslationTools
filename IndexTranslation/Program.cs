using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace IndexTranslation
{
    class Program
    {

        static void Main(string[] args)
        {
            #region Prepare
            Console.WriteLine("------------------------------------");
            Console.WriteLine("欢迎使用Stellaris翻译索引器");
            Console.WriteLine("    它的功能是对翻译编号, 便于调试. 请放\n" +
                              "到localisation的上一级目录, 二十四个文件\n必须完整." +
                              "\n发现bug请直接反馈给void QQ:296263103, \n");
            Console.WriteLine("------------------------------------");
            Console.WriteLine("现在按下任意键开始初始化.");
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
            Console.WriteLine("程序已经就绪, 不会覆盖原文件, 但也要记得做好备份! 按下任意键继续.");
            Console.ReadKey();
            var resultList = new List<TranslationItem>();
            int num = 0;
            var indexedPath = Environment.CurrentDirectory + "\\indexed\\";
            if (!Directory.Exists(indexedPath))
            {
                Directory.CreateDirectory(indexedPath);
            }
            else
            {
                foreach (var file in  new DirectoryInfo(indexedPath).GetFiles())
                {
                    file.Delete();
                }
            }
            foreach (var checkingFile in fileList)
            {
                Console.Write("处理文件 " + checkingFile + "...");
                var fileName = locDir + "\\" + checkingFile;
                try
                {
                    var fs = File.Open(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                    var sr = new StreamReader(fs, Encoding.UTF8);
                   
                    
                    var sw = new StreamWriter(new FileStream(indexedPath + checkingFile, FileMode.Create),Encoding.UTF8);
                    string line;
                    int lineNum = 0;
                    while ((line = sr.ReadLine()) != null)
                    {
                        lineNum ++;
                        if (!line.StartsWith("#") && line.Contains(":") && line.EndsWith("\""))
                        {

                            sw.WriteLine(line.Substring(0, line.Length - 1) + ","+num+"\"");
                            resultList.Add(new TranslationItem(num,checkingFile,lineNum));
                            num++;
                        }
                        else
                        {
                            sw.Write(line + "\n");
                        }
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
            Console.WriteLine("所有文件处理完毕, 正在保存翻译项目映射表...");
            Console.WriteLine("映射表保存完毕, 一切顺利, 可以退出了.");
            var mpsw = new StreamWriter(new FileStream("Mapping", FileMode.Create));
            foreach (var item in resultList)
            {
                mpsw.WriteLine(item.ToString());
            }
            mpsw.Flush();
            Console.ReadKey();

        }
        
        struct TranslationItem
        {
            public int id;
            public string file;
            public int line;

            public TranslationItem(int id, string file, int line)
            {
                this.id = id;
                this.file = file;
                this.line = line;
            }

            public override string ToString()
            {
                return $"{id},{file},{line}";
            }
        }
    }
}
