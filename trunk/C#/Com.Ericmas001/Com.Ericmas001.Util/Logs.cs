using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Com.Ericmas001.Util
{
    public class Logs
    {
        private static Dictionary<TraceListener, SourceLevels> m_Listeners = new Dictionary<TraceListener, SourceLevels>();

        public static void AddListener(TraceListener listener, SourceLevels minimumLevel)
        {
            if (minimumLevel == SourceLevels.Off)
                RemoveListener(listener);

            if (m_Listeners.ContainsKey(listener))
                m_Listeners[listener] = minimumLevel;
            else
                m_Listeners.Add(listener, minimumLevel);

        }
        public static void RemoveListener(TraceListener listener)
        {
            if (m_Listeners.ContainsKey(listener))
                m_Listeners.Remove(listener);
        }

        public static void Log(SourceLevels level, string message, params object[] args)
        {
            foreach (TraceListener listener in m_Listeners.Keys)
            {
                if (m_Listeners[listener] == SourceLevels.All || level <= m_Listeners[listener])
                {
                    listener.WriteLine(String.Format("[{0:yyyy-MM-dd HH:mm:ss.fff}] [{1}] {2}", DateTime.Now, level.ToString(), String.Format(message, args)));
                    listener.Flush();
                }
            }
        }

        public static void LogInformation(string information, params object[] args)
        {
            Log(SourceLevels.Information, information, args);
        }

        public static void LogWarning(string warning, params object[] args)
        {
            Log(SourceLevels.Warning, warning, args);
        }

        public static void LogError(string error, params object[] args)
        {
            Log(SourceLevels.Error, error, args);
        }

        public static void LogCriticalError(string error, params object[] args)
        {
            Log(SourceLevels.Critical, error, args);
        }

        public static void LogDebugInformation(string debugInfo, params object[] args)
        {
            Log(SourceLevels.Verbose, debugInfo, args);
        }
    }
}
