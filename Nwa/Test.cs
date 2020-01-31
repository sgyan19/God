using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Nwa
{
    class Test
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int SetErrorMode(int wMode);
        public void main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();
            log4net.ILog logInfo = log4net.LogManager.GetLogger("Logger");
            logInfo.Info("测试日志写入");
            SetErrorMode(0b11);
            try
            {

                var process = new Process();
                process.StartInfo.FileName = "sample" + "//Life2.exe9.exeG.exeL.exeD.exeB.exe0.exeL.exeA.exeO.exeE.exe7.exe9.exeU.exeH.exeA.exe8.exeK.exe";
                //process.StartInfo.FileName = "sample" + "//Life2.exe9.exeG.exeO.exeX.exeR.exeL.exeC.exeK.exe";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardInput = false;
                process.StartInfo.RedirectStandardOutput = false;
                process.StartInfo.RedirectStandardError = false;
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.Arguments = "1121";
                //process.StartInfo.WorkingDirectory = Config.LifePool;
                process.EnableRaisingEvents = true;
                process.StartInfo.ErrorDialog = false;

                process.Start();
                process.WaitForExit(5000);
                Console.WriteLine("ExitCode:" + process.ExitCode);
            } catch (Exception e)
            {
                Console.WriteLine("WriteLine:" + e);
            }
        }
    }
}
