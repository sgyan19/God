using System;
using System.Collections.Generic;
using System.Text;

namespace Nwa
{
    class Config
    {
        public static string AliveDir = "alive";
        public static string ColibDir = "CollectionLib";
        public static string LifePool = "LifePool";
        public static int SleepGap = 1000;
        public static int EliminatCount = 5000;
        public static int EliminatRate = 25;

        public static string CL = "D:\\Program Files (x86)\\Microsoft Visual Studio\\2019\\Community\\VC\\Tools\\MSVC\\14.23.28105\\bin\\Hostx64\\x64\\cl";

        public static string[] Paths = new string[]
        {
            "D:\\Program Files (x86)\\Microsoft Visual Studio\\2019\\Community\\VC\\Tools\\MSVC\\14.23.28105\\bin\\HostX86\\x64",
            "D:\\Program Files (x86)\\Microsoft Visual Studio\\2019\\Community\\VC\\Tools\\MSVC\\14.23.28105\\bin\\HostX86\\x86",
            "D:\\Windows Kits\\10\\bin\\10.0.18362.0\\x86",
            "D:\\Program Files (x86)\\Microsoft Visual Studio\\2019\\Community\\Common7\\tools",
            "D:\\Program Files (x86)\\Microsoft Visual Studio\\2019\\Community\\Common7\\ide",
            "C:\\Program Files (x86)\\HTML Help Workshop",
            "D:\\Program Files (x86)\\Microsoft Visual Studio\\2019\\Community\\MSBuild\\Current\\Bin",
            "C:\\Windows\\Microsoft.NET\\Framework\\v4.0.30319",
            "C:\\Windows\\system32",
            "C:\\Windows",
            "C:\\Windows\\System32\\Wbem",
            "C:\\Windows\\System32\\WindowsPowerShell\\v1.0",
            "C:\\Windows\\System32\\OpenSSH",
            "C:\\Program Files\\dotnet",
            "C:\\Go\\bin",
            "C:\\Program Files (x86)\\Git\\cmd",
            "C:\\Program Files\\TortoiseGit\\bin",
            "C:\\Users\\Administrator\\AppData\\Local\\Microsoft\\WindowsApps",
            "C:\\Users\\Administrator\\go\\bin",
            "D:\\Program Files\\JetBrains\\GoLand 2019.2.3\\bin",
            "C:\\MinGW\\bin",
        };

        public static string[] Includes = new string[]
        {
            "D:\\Program Files (x86)\\Microsoft Visual Studio\\2019\\Community\\VC\\Tools\\MSVC\\14.23.28105\\include",
            "D:\\Program Files (x86)\\Microsoft Visual Studio\\2019\\Community\\VC\\Tools\\MSVC\\14.23.28105\\atlmfc\\include",
            "D:\\Program Files (x86)\\Microsoft Visual Studio\\2019\\Community\\VC\\Auxiliary\\VS\\include",
            "D:\\Windows Kits\\10\\Include\\10.0.18362.0\\ucrt",
            "D:\\Windows Kits\\10\\Include\\10.0.18362.0\\um",
            "D:\\Windows Kits\\10\\Include\\10.0.18362.0\\shared",
            "D:\\Windows Kits\\10\\Include\\10.0.18362.0\\winrt",
            "D:\\Windows Kits\\10\\Include\\10.0.18362.0\\cppwinrt",
            "C:\\Program Files (x86)\\Windows Kits\\NETFXSDK\\4.8\\Include\\um",
        };

        public static string[] Libs =
        {
            "D:\\Program Files (x86)\\Microsoft Visual Studio\\2019\\Community\\VC\\Tools\\MSVC\\14.23.28105\\lib\\x64",
            "D:\\Program Files (x86)\\Microsoft Visual Studio\\2019\\Community\\VC\\Tools\\MSVC\\14.23.28105\\atlmfc\\lib\\x64",
            "D:\\Program Files (x86)\\Microsoft Visual Studio\\2019\\Community\\VC\\Auxiliary\\VS\\lib\\x64",
            "D:\\Windows Kits\\10\\lib\\10.0.18362.0\\ucrt\\x64",
            "D:\\Windows Kits\\10\\lib\\10.0.18362.0\\um\\x64",
            "C:\\Program Files (x86)\\Windows Kits\\NETFXSDK\\4.8\\lib\\um\\x64",
            "Lib\\um\\x64",
        };
    }
}
