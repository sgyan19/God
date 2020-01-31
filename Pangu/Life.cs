using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Pangu
{
    enum LifeStatus
    {
        BORNING, ALIVE, DEAD, HEAVEN
    }

    class Life
    {
        public string DNA {get { return dna; }}
        private readonly string dna;
        public string EXEName { get { return exe_name; } }
        private readonly string exe_name;
        public string EXEPath { get { return exe_path; } }
        private readonly string exe_path;
        public string DNAPath { get { return dna_path; } }
        private readonly string dna_path;
        public LifeStatus Status { get { return status; } }
        private LifeStatus status;
        private Incubator incubator;
        private Process process;
        public Life(string dna)
        {
            this.dna = dna;
            status = LifeStatus.BORNING;
            exe_name = dna + ".exe";
            dna_path = Config.AliveDir + "\\" + dna;
            exe_path = Config.AliveDir + "\\" + EXEName;
            incubator = new Incubator();
            incubator.AsyncBorn(dna_path, exe_path);
        }

        public bool Live()
        {
            if (status == LifeStatus.BORNING)
            {
                var boResult = incubator.CheckResult();
                if (boResult == null)
                {
                    return true;
                }

                if (boResult == true && File.Exists(EXEPath))
                {
                    Console.WriteLine(dna + " is borned!");
                    try
                    {
                        process = new Process();
                        process.StartInfo.FileName = exe_name;
                        process.StartInfo.UseShellExecute = true;
                        process.StartInfo.RedirectStandardInput = false;
                        process.StartInfo.RedirectStandardOutput = false;
                        process.StartInfo.RedirectStandardError = false;
                        process.StartInfo.CreateNoWindow = true;
                        process.StartInfo.WorkingDirectory = Config.AliveDir;
                        process.EnableRaisingEvents = true;
                        process.Exited += new EventHandler(myProcess_Exited);

                        process.Start();
                        status = LifeStatus.ALIVE;
                        return true;
                    } catch (Exception e)
                    {}
                }
                Console.WriteLine(dna + " borned failed!");
                status = LifeStatus.DEAD;
                return false;
            }
            if (status == LifeStatus.ALIVE)
            {
                try
                {
                    CheckOut();
                    return false;
                } catch (InvalidOperationException)
                {
                    return true;
                }
            }
            return false;
        }

        private void myProcess_Exited(object sender, System.EventArgs e)
        {
            CheckOut();
        }
        private void CheckOut()
        {
            status = process.ExitCode == 0 ? LifeStatus.HEAVEN : LifeStatus.DEAD;
            if (status == LifeStatus.HEAVEN)
            {
                Console.WriteLine(dna + " is  goto heaven!");
            }
            else
            {
                Console.WriteLine(dna + " is  dead!");
            }
        }
        ~Life()
        {
            try
            {
                if (process != null)
                {
                    process.CancelErrorRead();
                    process.CancelOutputRead();
                    process.Close();
                }
            }catch(Exception) { }
        }
    }
 }
