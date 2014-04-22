using System;
using System.Drawing;
using System.Windows.Forms;

namespace Com.Ericmas001.Windows.Forms
{
    internal class WindowsConsole : RichTextBox
    {
        public new string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
            }
        }

        public WindowsConsole()
        {
            ReadOnly = true;
            BackColor = Color.Black;
            Font = new Font("Courier New", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.White;
        }

        private delegate void DelegateWrite(string s);

        public void Write(string from, string s, bool heure)
        {
            Write(String.Format("<{0}> {1}", from, s), heure);
        }

        public void Write(string from, string s)
        {
            Write(from, s, true);
        }

        public void Write(string s)
        {
            Write(s, true);
        }

        public void Write(string s, bool heure)
        {
            try
            {
                if (InvokeRequired)
                {
                    Invoke(new DelegateWrite(Write), new object[] { s });
                    return;
                }
                var date = "";
                if (heure)
                    date = String.Format("[{0}] ", DateTime.Now);
                Text += date + s;

                SelectionStart = TextLength;
                Focus();
                ScrollToCaret();
            }
            catch (ObjectDisposedException)
            {
            }
        }

        public void WriteLine(string from, string s)
        {
            WriteLine(from, s, true);
        }

        public void WriteLine(string from, string s, bool heure)
        {
            WriteLine(String.Format("<{0}> {1}", from, s), heure);
        }

        public void WriteLine(string s)
        {
            WriteLine(s, true);
        }

        public void WriteLine(string s, bool heure)
        {
            Write(s + Environment.NewLine, heure);
        }
    }
}