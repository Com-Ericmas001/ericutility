using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace EricUtility.Windows.Forms
{
    public partial class NumberedTextBoxUC : UserControl
    {

        public delegate void EmptyHandler();
        public delegate void StringHandler(string value);
        public NumberedTextBoxUC()
        {
            InitializeComponent();
			numberLabel.Font = new Font( theRichTextBox.Font.FontFamily, theRichTextBox.Font.Size );
        }

        private void updateNumberLabel()
        {
			if ( this.InvokeRequired )
			{
				this.Invoke( new EmptyHandler( updateNumberLabel ), new object[] { } );
				return;
			}
			int firstLine = theRichTextBox.GetLineFromCharIndex( theRichTextBox.GetCharIndexFromPosition( Point.Empty ) );
			Point rightBottomCorner = new Point( ClientRectangle.Width, ClientRectangle.Height );
			int lastLine = theRichTextBox.GetLineFromCharIndex( theRichTextBox.GetCharIndexFromPosition( rightBottomCorner ) );
			
			if ( theRichTextBox.Text.EndsWith( "\n" ) )
				lastLine++;
           
            //finally, renumber label
			StringWriter sw = new StringWriter();
            for (int i = firstLine; i <= lastLine; i++)
				sw.WriteLine( i + 1 );
			numberLabel.Text = sw.ToString();
        }


		private void richTextBox1_TextChanged( object sender, EventArgs e )
		{
			if ( this.InvokeRequired )
			{
				this.Invoke( new EventHandler( richTextBox1_TextChanged ), new object[] { sender, e } );
				return;
			}
			theRichTextBox.Select( 0, 0 );
			theRichTextBox.ScrollToCaret();
			richTextBox1_VScroll( sender, e );
		}

		private void richTextBox1_VScroll( object sender, EventArgs e )
		{

			if ( this.InvokeRequired )
			{
				this.Invoke( new EventHandler( richTextBox1_VScroll ), new object[] { sender, e } );
				return;
			}
			//move location of numberLabel for amount of pixels caused by scrollbar
			int d = theRichTextBox.GetPositionFromCharIndex( 0 ).Y % ( theRichTextBox.Font.Height + 1 );
			numberLabel.Location = new Point( 0, d );

			updateNumberLabel();
		}
		public void SetText( string s )
		{
			if ( this.InvokeRequired )
			{
				this.Invoke( new StringHandler( SetText ), new object[] { s } );
				return;
			}
			theRichTextBox.Text = s;
		}
    }
}
