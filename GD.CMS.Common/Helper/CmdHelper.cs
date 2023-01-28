using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace GD.CMS.Common
{
   public class CmdHelper
    {

        /// <summary>
        /// cmd 命令执行
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="workExe"></param>
        /// <returns></returns>
        public static string RunCmdForJobs(string cmdText,string workExe)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                using (Process p = new Process())
                {
                    p.StartInfo.FileName = "cmd.exe";
                    p.StartInfo.WorkingDirectory = workExe;// "D:\\ffmpeg\\bin";   //这个属性是设置CMD命令在哪个路径下执行,这里绑定我的视频元数据信息处理工具的路径。
                    p.StartInfo.UseShellExecute = false;        //是否使用操作系统shell启动 
                    p.StartInfo.RedirectStandardInput = true;   //接受来自调用程序的输入信息 
                    p.StartInfo.RedirectStandardOutput = true;  //由调用程序获取输出信息 
                    p.StartInfo.RedirectStandardError = true;   //重定向标准错误输出 
                    p.StartInfo.CreateNoWindow = false;          //不显示程序窗口 
                    p.Start();//启动程序 
                              //向cmd窗口写入命令 
                    p.StandardInput.WriteLine(cmdText + "&exit");
                    p.StandardInput.AutoFlush = true;
                    //获取cmd窗口的输出信息 
                    StreamReader reader = p.StandardOutput;//截取输出流 
                    string line = reader.ReadLine();//每次读取一行 
                    sb.Append(line + "\n");
                    while (!reader.EndOfStream)
                    {
                        line = reader.ReadLine();
                        sb.Append(line + "\n");
                    }
                    p.WaitForExit();//等待程序执行完退出进程 
                    p.Close();
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
          
        }
    }
}
