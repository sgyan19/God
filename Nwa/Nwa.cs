using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace Nwa
{
    enum LifeStatus
    {
        ALIVE, DEAD, HEAVEN
    }
    class Life
    {
        private Process process;

        private LifeStatus status;
        private string name;

        public Life(string name, string arg)
        {
            this.name = name;
            try
            {
                process = new Process();
                process.StartInfo.FileName = Config.LifePool + "//" + name;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardInput = false;
                process.StartInfo.RedirectStandardOutput = false;
                process.StartInfo.Arguments = arg;
                process.StartInfo.RedirectStandardError = false;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                //process.StartInfo.WorkingDirectory = Config.LifePool;
                process.EnableRaisingEvents = true;
                process.StartInfo.ErrorDialog = false;
                process.Exited += new EventHandler(myProcess_Exited);

                process.Start();
                process.WaitForExit(5000);
                status = LifeStatus.ALIVE;
            }
            catch (Exception e)
            {
                status = LifeStatus.DEAD;
            }
        }

        public bool CheckLive()
        {
            if (status == LifeStatus.ALIVE)
            {
                try
                {
                    CheckExitCode();
                    return false;
                }
                catch (Exception e)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsHeaven()
        {
            return status == LifeStatus.HEAVEN;
        }

        private void myProcess_Exited(object sender, System.EventArgs e)
        {
            if (status == LifeStatus.ALIVE)
            {
                CheckExitCode();
            }
        }

        private void CheckExitCode()
        {
            status = process.ExitCode == 0 ? LifeStatus.HEAVEN : LifeStatus.DEAD;
            if (status == LifeStatus.HEAVEN)
            {
                Nwa.logInfo.Info(name + " is  goto heaven!");
            }
            else
            {
                Nwa.logInfo.Info(name + " is  dead!");
            }
            try
            {
                process.Kill();
            }
            catch (Exception e) { }
        }
    }

    class Nwa
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int SetErrorMode(int wMode);

        private Dictionary<string, Life> lifes = new Dictionary<string, Life>();

        public static log4net.ILog logInfo;
        public void main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();
            logInfo = log4net.LogManager.GetLogger("Logger");
            SetErrorMode(0b11);
            Random rd = new Random((int)((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalMilliseconds));
            rd.Next();
            if (!Directory.Exists(Config.LifePool))
            {
                Directory.CreateDirectory(Config.LifePool);
            }

            for (; ; )
            {
                Dictionary<string, Life> tmp = new Dictionary<string, Life>(lifes);
                
                foreach (KeyValuePair<string, Life> lifePair in tmp)
                {
                    if (!lifePair.Value.CheckLive())
                    {
                        lifes.Remove(lifePair.Key);
                        if (!lifePair.Value.IsHeaven())
                        {
                            File.Delete(Config.LifePool + "\\" + lifePair.Key);
                        }
                    }
                }

                var files = Directory.GetFiles(Config.LifePool);
                while (files.Length > Config.EliminatCount)
                {
                    logInfo.Info("start eliminating!");
                    foreach (var file in files)
                    {
                        string name = Path.GetFileName(file);
                        if (name.Equals("Life2.exe"))
                        {
                            continue;
                        }
                        var live = rd.Next(0, Config.EliminatRate);
                        if (live != 1)
                        {
                            logInfo.Info(name + " die out");
                            try
                            {
                                File.Delete(Path.GetFullPath(file));
                            }
                            catch (Exception e) { }
                        }
                    }
                    files = Directory.GetFiles(Config.LifePool);
                }
                logInfo.Info("alive count：" + files.Length);
                if (files.Length != 0)
                {
                    foreach (var file in files)
                    {
                        string name = Path.GetFileName(file);
                        if (!name.EndsWith(".exe"))
                        {
                            string fullPath = Path.GetFullPath(file) + ".exe";
                            name = name + ".exe";
                            try
                            {
                                File.Move(Path.GetFullPath(file), fullPath);
                            }
                            catch (Exception i)
                            {
                                continue;
                            }
                        }
                        if (!lifes.ContainsKey(name))
                        {
                            lifes.Add(name, new Life(name, rd.Next().ToString()));
                        }
                    }
                }
                Thread.Sleep(1000);
            }
        }

    }

}