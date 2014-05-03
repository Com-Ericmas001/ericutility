namespace Com.Ericmas001.Windows.Forms
{
    partial class NumberedTextBoxUc
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.numberLabel = new System.Windows.Forms.Label();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.theRichTextBox = new System.Windows.Forms.RichTextBox();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.SuspendLayout();
			// 
			// numberLabel
			// 
			this.numberLabel.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
						| System.Windows.Forms.AnchorStyles.Left )
						| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.numberLabel.Font = new System.Drawing.Font( "Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 238 ) ) );
			this.numberLabel.Location = new System.Drawing.Point( 3, 0 );
			this.numberLabel.Name = "numberLabel";
			this.numberLabel.Size = new System.Drawing.Size( 35, 217 );
			this.numberLabel.TabIndex = 1;
			this.numberLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer1.IsSplitterFixed = true;
			this.splitContainer1.Location = new System.Drawing.Point( 0, 0 );
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add( this.splitContainer2 );
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add( this.theRichTextBox );
			this.splitContainer1.Size = new System.Drawing.Size( 403, 229 );
			this.splitContainer1.SplitterDistance = 41;
			this.splitContainer1.SplitterWidth = 1;
			this.splitContainer1.TabIndex = 2;
			this.splitContainer1.Text = "splitContainer1";
			// 
			// theRichTextBox
			// 
			this.theRichTextBox.BackColor = System.Drawing.Color.White;
			this.theRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.theRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.theRichTextBox.Font = new System.Drawing.Font( "Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 238 ) ) );
			this.theRichTextBox.Location = new System.Drawing.Point( 0, 0 );
			this.theRichTextBox.Name = "theRichTextBox";
			this.theRichTextBox.ReadOnly = true;
			this.theRichTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
			this.theRichTextBox.Size = new System.Drawing.Size( 361, 229 );
			this.theRichTextBox.TabIndex = 0;
			this.theRichTextBox.Text = "";
			this.theRichTextBox.WordWrap = false;
			this.theRichTextBox.VScroll += new System.EventHandler( this.richTextBox1_VScroll );
			this.theRichTextBox.TextChanged += new System.EventHandler( this.richTextBox1_TextChanged );
			// 
			// splitContainer2
			// 
			this.splitContainer2.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
						| System.Windows.Forms.AnchorStyles.Left )
						| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.splitContainer2.IsSplitterFixed = true;
			this.splitContainer2.Location = new System.Drawing.Point( 0, 0 );
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add( this.numberLabel );
			this.splitContainer2.Panel1MinSize = 5;
			this.splitContainer2.Panel2MinSize = 5;
			this.splitContainer2.Size = new System.Drawing.Size( 38, 229 );
			this.splitContainer2.SplitterDistance = 217;
			this.splitContainer2.TabIndex = 2;
			// 
			// NumberedTextBoxUc
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add( this.splitContainer1 );
			this.Name = "NumberedTextBoxUc";
			this.Size = new System.Drawing.Size( 403, 229 );
			this.splitContainer1.Panel1.ResumeLayout( false );
			this.splitContainer1.Panel2.ResumeLayout( false );
			this.splitContainer1.ResumeLayout( false );
			this.splitContainer2.Panel1.ResumeLayout( false );
			this.splitContainer2.ResumeLayout( false );
			this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.RichTextBox theRichTextBox;
        private System.Windows.Forms.Label numberLabel;
        private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.SplitContainer splitContainer2;
    }
}
