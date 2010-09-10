/***********************************************\
|***********************************************|
|********** OEBIUS SOURCE MANAGER **************|
|***********************************************|
|****** Oebius < Http://www.oebius.com > *******|
|***********************************************|
|********* Merlouze ( Éric Massé ) *************|
|********* ericmas001@hotmail.com **************|
|***********************************************|
\***********************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace EricUtility.Windows.Forms
{
	public class OpenFolderTextBox : OpenFileTextBox
	{
		private System.Windows.Forms.FolderBrowserDialog myOpenFolderDialog;

		public OpenFolderTextBox() : base()
		{
			InitializeComponent();
		}
		private void InitializeComponent()
		{
			this.myOpenFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.SuspendLayout();
			// 
			// OpenFolderTextBox
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
			this.Name = "OpenFolderTextBox";
			this.ResumeLayout( false );
			this.PerformLayout();

		}
		protected override void OnOpenClick()
		{
			myOpenFolderDialog.SelectedPath = Value;
			if ( myOpenFolderDialog.ShowDialog() == DialogResult.OK )
				Value = myOpenFolderDialog.SelectedPath;
		}
	}
}
