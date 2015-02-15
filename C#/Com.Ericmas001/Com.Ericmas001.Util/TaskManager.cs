using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Com.Ericmas001.Util
{
    public class TaskManager
    {
        public static void KillAll(string pgmName)
        {
            List<Process> toKill = new List<Process>(Process.GetProcessesByName(pgmName));
            bool killed = false;
            while (toKill.Count > 0)
            {
                Process p = toKill[0];
                if (killed)
                {
                    if (!p.HasExited)
                    {
                        Thread.Sleep(500);
                    }
                    else
                    {
                        killed = false;
                        toKill.RemoveAt(0);
                    }
                }
                else
                {
                    killed = true;
                    try
                    {
                        p.Kill();
                    }
                    catch { };
                }
            }
        }
        public static void KillAllAccess()
        {
            KillAll("MSACCESS");
        }
    }
}
