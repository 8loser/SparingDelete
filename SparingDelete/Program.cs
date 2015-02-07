using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using System.IO;
using System.Collections;
using Microsoft.VisualBasic.FileIO;
using CommandLine;
using CommandLine.Text;

namespace SparingDelete
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new Options();
            if (CommandLine.Parser.Default.ParseArguments(args, options))
            {
                Process(options.folder,options.pattern,options.keep,options.recycling,options.log);
            }
        }

        //執行刪除
        private static void Process(string root, string Pattern, int KeepNumbers, Boolean RecycleBin,Boolean LogWrite)
        {
            if (PathCheck(root))    //判斷備份檔路徑是否存在
            {
                string[] SubdirectoryList = GetSubdirectoryList(root);  //取得備份檔目錄內子目錄清單
                foreach (string dir in SubdirectoryList )
                {
                    Console.WriteLine("清除資料夾：" + dir);
                    if (PathCheck(dir))     //判斷子目錄是否存在
                    {
                        string[] FileList = GetFileList(dir, Pattern);   //取得子目錄內檔案(*.bak)清單
                        if (FileList.Length > KeepNumbers)      //如果子目錄檔案數量大於備份檔保留個數
                        {
                            string[] DeleteFileList = FileList.Take(FileList.Length - KeepNumbers).ToArray();   //排除保留個數外將刪除檔案清單
                            foreach (string DeleteFile in DeleteFileList)
                            {
                                if (PathCheck(DeleteFile))  //如果檔案存在
                                {
                                    if (RecycleBin) //是否移至資源回收筒
                                    {
                                        FileSystem.DeleteFile(DeleteFile, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin); //移到資源回收筒
                                        Console.WriteLine("移至資源回收筒：" + DeleteFile);
                                    }
                                    else
                                    {
                                        File.Delete(DeleteFile);    //刪除檔案
                                        Console.WriteLine("刪除檔案：" + DeleteFile);
                                    }

                                    if (LogWrite)   //是否產生紀錄檔
                                    {
                                        //寫入紀錄檔BackupClearLog+日期+.txt, 如檔案已存在則附加
                                        using (StreamWriter LogWriting = new StreamWriter("BackupClearLog " + DateTime.Now.ToString("yyyy-MM-dd") + ".txt", true))
                                        { LogWriting.WriteLine(DeleteFile); }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        //判斷路徑是否存在
        private static Boolean PathCheck(string path)
        {
            if (File.Exists(path))  //路徑為檔案回傳true
            { return true; }
            else if (Directory.Exists(path))    //路徑為目錄回傳true
            { return true; }
            else
            {
                Console.WriteLine("路徑不存在");
                return false;
            }
        }

        //取得目錄內檔案清單
        private static string[] GetFileList(string path, string Pattern)
        {
            //取得路徑內副檔名為bak清單(不分大小寫), 並依建立時間早至晚排序
            string[] files = Directory.GetFiles(path, Pattern).OrderBy(p => new FileInfo(p).CreationTime).ToArray();
            return files;
        }

        //取得子目錄清單
        private static string[] GetSubdirectoryList(string root)
        {   
            //取得路徑內子目錄清單, 不包含更內層目錄
            string[] dirs = Directory.GetDirectories(root).Where(d => !d.EndsWith("System Volume Information")).ToArray();
            return dirs;
        }
    }
}
