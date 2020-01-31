using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace Pangu
{
    namespace Nvwa
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
                    } catch (Exception e)
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
                    Console.WriteLine(name + " is  goto heaven!");
                }
                else
                {
                    Console.WriteLine(name + " is  dead!");
                }
                process.Kill();
            }
        }

        class Nvwa
        {
            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern int SetErrorMode(int wMode);

            private Dictionary<string, Life> lifes = new Dictionary<string, Life>();
            public void main(string[] args)
            {
                SetErrorMode(0b11);
                Random rd = new Random((int)((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalMilliseconds));
                rd.Next();
                if (!Directory.Exists(Config.LifePool))
                {
                    Directory.CreateDirectory(Config.LifePool);
                }

                for (; ; )
                {
                    foreach (KeyValuePair<string, Life> lifePair in lifes)
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
                        Console.WriteLine("start eliminating!");
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
                                Console.WriteLine("name die out");
                                try
                                {
                                    File.Delete(Path.GetFullPath(file));
                                } catch (Exception e) { }
                            }
                        }
                        files = Directory.GetFiles(Config.LifePool);
                    }
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
                                    File.Move(Path.GetFullPath(file), fullPath, true);
                                } catch (Exception i)
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
}
