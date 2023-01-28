using System;
using System.IO;
using System.Threading.Tasks;

namespace GD.CMS.Common
{
    /// <summary>
    /// 日志帮助类
    /// </summary>
    public static class LogHelper
    {
        /// <summary>
        /// 写入日志到本地TXT文件
        /// 注：日志文件名为"A_log.txt",目录为根目录
        /// </summary>
        /// <param name="log">日志内容</param>
        public static void WriteLog_LocalTxt(string log)
        {
            Task.Run(() =>
            {
                try
                {
                    string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "\\Erro\\" + DateTime.Now.ToCstTime().ToString("yyyyMMdd") + "\\", "gd_loger.txt");
                    string logContent = $"{DateTime.Now.ToCstTime().ToString("yyyy-MM-dd HH:mm:ss")}:{log}\r\n";
                    FileHelper.WriteTxt(logContent,filePath);
                }
                finally 
                {
                    
                }
            });
        }
    }
}
