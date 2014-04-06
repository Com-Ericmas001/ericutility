
namespace Com.Ericmas001.Windows.Forms
{
	partial class OpenFileTextBox
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose( bool disposing )
		{
			if ( disposing && ( components != null ) )
			{
				components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.txtPath = new System.Windows.Forms.TextBox();
			this.btnOpen = new System.Windows.Forms.Button();
			this.myOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.SuspendLayout();
			// 
			// txtPath
			// 
			this.txtPath.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
						| System.Windows.Forms.AnchorStyles.Left )
						| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.txtPath.Location = new System.Drawing.Point( 0, 2 );
			this.txtPath.Name = "txtPath";
			this.txtPath.Size = new System.Drawing.Size( 323, 20 );
			this.txtPath.TabIndex = 15;
			this.txtPath.TextChanged += new System.EventHandler( this.txtPath_TextChanged );
			// 
			// btnOpen
			// 
			this.btnOpen.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.btnOpen.Image = global::Com.Ericmas001.Windows.Forms.Properties.Resources.Open_Folder_Small;
			this.btnOpen.Location = new System.Drawing.Point( 329, 0 );
			this.btnOpen.Name = "btnOpen";
			this.btnOpen.Size = new System.Drawing.Size( 31, 23 );
			this.btnOpen.TabIndex = 16;
			this.btnOpen.UseVisualStyleBackColor = true;
			this.btnOpen.Click += new System.EventHandler( this.btnOpen_Click );
			// 
			// myOpenFileDialog
			// 
			this.myOpenFileDialog.Filter = "Tous les fichiers (*.*)|*.*";
			// 
			// OpenFileTextBox
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add( this.btnOpen );
			this.Controls.Add( this.txtPath );
			this.Name = "OpenFileTextBox";
			this.Size = new System.Drawing.Size( 363, 23 );
			this.ResumeLayout( false );
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtPath;
		private System.Windows.Forms.Button btnOpen;
		private System.Windows.Forms.OpenFileDialog myOpenFileDialog;
	}
}
