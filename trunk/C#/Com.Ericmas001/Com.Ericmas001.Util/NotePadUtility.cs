using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ericmas001.Util
{
    public class NotePadUtility
    {
        [DllImport("user32.dll", EntryPoint = "FindWindowEx")]
        private static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
        [DllImport("User32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int uMsg, int wParam, string lParam);
        [DllImport("User32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int uMsg, int wParam, int lParam);

        public static void SendToNotePad(string text)
        {
            Process p = Process.Start("notepad");

            // Attends que notepad soit prêt
            p.WaitForInputIdle();

            IntPtr child = FindWindowEx(p.MainWindowHandle, new IntPtr(0), "Edit", null);

            // Envoi le texte dans notepad
            SendMessage(child, 0x00B1, 0, 0);
            SendMessage(child, 0x00C2, 1, text);

            if (!String.IsNullOrEmpty(text))
            {
                // Repositionne le curseur au début du fichier
                SendMessage(child, 0x00B1, 0, 1);
                SendMessage(child, 0x00C2, 1, text[0].ToString());
            }

        }
    }
}
