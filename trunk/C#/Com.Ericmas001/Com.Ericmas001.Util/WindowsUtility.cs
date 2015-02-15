using System;
using System.IO;
using System.Reflection;

namespace Com.Ericmas001.Util
{
    public static class WindowsUtility
    {
        public static bool IsWindows7OrHigher()
        {
            OperatingSystem osInfo = Environment.OSVersion;
            return (osInfo.Platform == PlatformID.Win32NT && osInfo.Version >= Version.Parse("6.1"));
        }

        public static string AppLocalDataPath()
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string appName = Assembly.GetEntryAssembly().GetName().Name;
            string res = Path.Combine(appDataPath, appName);
            if (!Directory.Exists(res))
                Directory.CreateDirectory(res);
            return res;
        }
    }
}
