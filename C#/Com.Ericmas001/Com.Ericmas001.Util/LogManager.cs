using Com.Ericmas001.Portable.Util;
using System;
using System.IO;

namespace Com.Ericmas001.Util
{

    public static class LogManager
    {

        public static void LogInConsole(string from, string message, int level, LogLevel minLevelToLog)
        {
            var fc = Console.ForegroundColor;
            var bc = Console.BackgroundColor;


            //Errors
            if (level >= (int)LogLevel.ErrorHigh)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                message = "ERROR: " + message;
            }
            if (level >= (int)LogLevel.Error)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                message = "ERROR: " + message;
            }
            else if (level >= (int)LogLevel.ErrorLow)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
            }


            //Warnings
            else if (level >= (int)LogLevel.Warning)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                message = "WARNING: " + message;
            }
            else if (level >= (int)LogLevel.WarningLow)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
            }


            //Messages
            else if (level >= (int)LogLevel.MessageVeryHigh)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                message = "IMPORTANT: " + message;
            }
            else if (level >= (int)LogLevel.Message)
            {
                Console.ForegroundColor = ConsoleColor.White;
            }
            else if (level >= (int)LogLevel.MessageLow)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                message = "DEBUG: " + message;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                message = "DEBUG: " + message;
            }


            //Let's Log!
            if (level >= (int)minLevelToLog)
                Console.WriteLine(message);

            Console.ForegroundColor = fc;
            Console.BackgroundColor = bc;
        }
    }
}
