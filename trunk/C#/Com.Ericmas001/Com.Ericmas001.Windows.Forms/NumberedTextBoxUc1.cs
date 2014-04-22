using Com.Ericmas001.Util;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Com.Ericmas001.Windows.Forms
{
    public partial class NumberedTextBoxUc1 : UserControl
    {
        public NumberedTextBoxUc1()
        {
            InitializeComponent();
            numberLabel.Font = new Font(theRichTextBox.Font.FontFamily, theRichTextBox.Font.Size);
        }

        private void UpdateNumberLabel()
        {
            if (InvokeRequired)
            {
                Invoke(new EmptyHandler(UpdateNumberLabel), new object[] { });
                return;
            }
            var firstLine = theRichTextBox.GetLineFromCharIndex(theRichTextBox.GetCharIndexFromPosition(Point.Empty));
            var rightBottomCorner = new Point(ClientRectangle.Width, ClientRectangle.Height);
            var lastLine = theRichTextBox.GetLineFromCharIndex(theRichTextBox.GetCharIndexFromPosition(rightBottomCorner));

            if (theRichTextBox.Text.EndsWith("\n"))
                lastLine++;

            //finally, renumber label
            var sw = new StringWriter();
            for (var i = firstLine; i <= lastLine; i++)
                sw.WriteLine(i + 1);
            numberLabel.Text = sw.ToString();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler(richTextBox1_TextChanged), new[] { sender, e });
                return;
            }
            theRichTextBox.Select(0, 0);
            theRichTextBox.ScrollToCaret();
            richTextBox1_VScroll(sender, e);
        }

        private void richTextBox1_VScroll(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler(richTextBox1_VScroll), new[] { sender, e });
                return;
            }

            //move location of numberLabel for amount of pixels caused by scrollbar
            var d = theRichTextBox.GetPositionFromCharIndex(0).Y % (theRichTextBox.Font.Height + 1);
            numberLabel.Location = new Point(0, d);

            UpdateNumberLabel();
        }

        public void SetText(string s)
        {
            if (InvokeRequired)
            {
                Invoke(new StringHandler(SetText), new object[] { s });
                return;
            }
            theRichTextBox.Text = s;
        }
    }
}