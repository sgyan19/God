using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Pangu
{
    class Incubator
    {
        private string PATH;
        private string INCLUDE;
        private string LIB;
        private string Compiler;
        private string CMD_PATH;
        private string CMD_INCLUDE;
        private string CMD_LIB;
        private string CMD_COM_TMP;

        private Process process;

        private bool? result;
        private Task t1, t2;
        private string dnaPath;
        private string exePath;
        private string boObjPath;
        private string boExePath;

        public Incubator()
        {
            foreach (string i in Config.Paths)
            {
                PATH = PATH + i + ";";
            }
            CMD_PATH = "set PATH=%PATH;" + PATH;

            foreach (var i in Config.Includes)
            {
                INCLUDE = INCLUDE + i + ";";
            }
            CMD_INCLUDE = "set INCLUDE=" + INCLUDE;

            foreach (var i in Config.Libs)
            {
                LIB = LIB + i + ";";
            }
            CMD_LIB = "set LIB=" + LIB;

            Compiler = Config.CL;

            CMD_COM_TMP = "\"" + Compiler + "\" /TC ";
            result = null;
        }

        public bool? CheckResult()
        {
            if (result == null)
            {
                return result;
            }
            if (process != null) { 
                try
                {
                    process.Kill();
                    Console.WriteLine("born code:" + process.ExitCode);
                    process = null;
                } catch (Exception)
                {
                }
                return null;
            }
            if (File.Exists(boObjPath))
             {
                try
                {
                    File.Delete(boObjPath);
                }
                catch (Exception) { }
            }
            if (result == true && File.Exists(boExePath))
            {
                if (File.Exists(exePath))
                {
                    File.Delete(exePath);
                }
                try
                {
                    File.Move(boExePath, boExePath + ".tmp");
                    File.Move(boExePath + ".tmp", exePath);
                    //File.Copy(outFile, outFile + ".tmp");
                    //File.Move(outFile + ".tmp", exePath);
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace.ToString());
                    return null;
                }
            }
            if (File.Exists(boExePath))
            {
                try
                {
                    File.Delete(boExePath);
                }
                catch (Exception) { }
            }
            return false;
        }

        public void AsyncBorn(string dnaPath, string exePath)
        {
            this.dnaPath = dnaPath;
            string outFileName = Path.GetFileName(dnaPath);
            this.boObjPath = outFileName + ".obj";
            this.boExePath = outFileName + ".exe";
            this.exePath = exePath;
            try {
                process = new Process();
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.CreateNoWindow = false;
                process.Start();

                
                t1 = Task.Factory.StartNew(delegate
                {
                    string line;
                    while (result == null && (line = process.StandardOutput.ReadLine()) != null)
                    {
                        //Console.WriteLine("Incubator info:" + line);
                        if (line.StartsWith("/out:"))
                        {
                            result = true;
                            break;
                        }
                        if (line.Contains("error C") || line.Contains("error LNK")) // or link error
                        {
                            break;
                        }
                    }
                    if (result == null)
                    {
                        result = false;
                    }
                });
                /*
                t2 = Task.Factory.StartNew(delegate
                {
                    string line;
                   while (Result == null && (line = process.StandardError.ReadLine()) != null)
                    {
                        Console.WriteLine("Incubator error:" + line);
                    }
                });
                */

                process.StandardInput.WriteLine(CMD_PATH);
                process.StandardInput.WriteLine(CMD_INCLUDE);
                process.StandardInput.WriteLine(CMD_LIB);
                process.StandardInput.WriteLine(CMD_COM_TMP + dnaPath);
                process.StandardInput.Flush();
            }
            catch (Exception)
            {
                result = false;
            }
        }

        ~Incubator()
        {
            try
            {
                if (process != null)
                {
                    process.CancelErrorRead();
                    process.CancelOutputRead();
                    process.Close();
                }
            } catch(Exception) 
            { }
        }
    }
}
