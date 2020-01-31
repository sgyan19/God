using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace Pangu
{
    class Pangu
    {
        private bool InsureDirReady()
        {
            try
            {
                if (!Directory.Exists(Config.AliveDir))
                {
                    Directory.CreateDirectory(Config.AliveDir);
                }

                if (!Directory.Exists(Config.ColibDir))
                {
                    Directory.CreateDirectory(Config.ColibDir);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return false;
            }
            return true;
        }

        private Dictionary<string, Life> lifes;

        public void ProgramMain(string[] args)
        {
            Console.WriteLine("Hello World!");
            /*
            while (insureDirReady())
            {
                Debug.WriteLine("World prepare failed,sleep a duration thren try agin!");
                Thread.Sleep(5000);
            }
            */
            InsureDirReady();
            Console.WriteLine("All is ready.Now let's create a new world");
            lifes = new Dictionary<string, Life>();

            /*
            a = new Incubator();
            a.AsyncBorn("alive\\dna", "alive\\dna.exe");
            while(true)
            {
                Thread.Sleep(1000);
            }
            */
            Loop();
        }


        private void Loop()
        {
            for (; ; )
            {
                Lookup();
                Thread.Sleep(100);
            }
        }

        private void Lookup()
        {
            foreach (KeyValuePair<string, Life> lifePair in lifes)
            {
                if (!lifePair.Value.Live())
                {
                    lifes.Remove(lifePair.Key);
                    try
                    {
                        File.Delete(lifePair.Value.EXEPath);
                    }
                    catch (Exception) { }
                    if (lifePair.Value.Status == LifeStatus.HEAVEN)
                    {
                        var heavenPath = Config.ColibDir + "\\" + lifePair.Value.DNA;
                        if (File.Exists(heavenPath))
                        {
                            try
                            {
                                File.Delete(heavenPath);
                            }
                            catch (IOException) { }
                        }
                        try
                        {
                            File.Move(lifePair.Value.DNAPath, heavenPath);
                        }
                        catch (IOException) { }
                    }
                    else
                    {
                        File.Delete(lifePair.Value.DNAPath);
                    }
                }
            }

            var files = Directory.GetFiles(Config.AliveDir);
            if (files.Length == 0)
            {
                var angels = Directory.GetFiles(Config.ColibDir);
                Random rd = new Random();
                var r = rd.Next();
                var chose = angels[r % angels.Length];
                var choseName = Path.GetFileName(chose);
                File.Copy(chose, Config.AliveDir + "\\" + choseName);
            }
            else
            {

                foreach (var file in files)
                {
                    if (file.EndsWith(".exe"))
                    {
                        var index = file.LastIndexOf('\\');
                        if (index < 0) index = 0;
                        var dna = file.Substring(index + 1, file.Length - 4 - index - 1);

                        if (!lifes.ContainsKey(dna))
                        {
                            File.Delete(file);
                        }
                    }
                    else if (file.EndsWith(".obj"))
                    {
                        File.Delete(file);
                    }
                    else
                    {
                        var index = file.LastIndexOf('\\');
                        if (index < 0) index = 0;
                        var dna = file.Substring(index + 1, file.Length - index - 1);
                        if (!lifes.ContainsKey(dna))
                        {
                            lifes.Add(dna, new Life(dna));
                        }
                    }
                }
                /*
                var currentFiles = Directory.EnumerateFiles(".");
                foreach (var file in currentFiles)
                {
                    if (file.EndsWith(".exe") || file.EndsWith(".obj"))
                    {
                        try
                        {
                            File.Delete(file);
                        }
                        catch (Exception) { }
                    }
                }*/
            }
        }
    }
}
