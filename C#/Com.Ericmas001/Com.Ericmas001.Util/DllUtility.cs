using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ericmas001.Util
{
    public static class DllUtility
    {
        public static void Register(String dllPath)
        {
            Process process = Process.Start("regsvr32.exe", "\"" + dllPath + @""" /s");
            if (process != null) 
                process.WaitForExit();
        }

        public static void UnRegister(String dllPath)
        {
            Process process = Process.Start("regsvr32.exe", "\"" + dllPath + @""" /s /u");
            if (process != null) 
                process.WaitForExit();
        }
    }
}
