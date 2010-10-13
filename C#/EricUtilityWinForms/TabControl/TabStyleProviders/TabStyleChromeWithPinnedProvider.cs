/*
 * This code is provided under the Code Project Open Licence (CPOL)
 * See http://www.codeproject.com/info/cpol10.aspx for details
 */

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace EricUtility.Windows.Forms
{
	[System.ComponentModel.ToolboxItem(false)]
    public class TabStyleChromeFirstPinnedProvider : TabStyleChromeProvider
	{
        public TabStyleChromeFirstPinnedProvider(CustomTabControl tabControl)
            : base(tabControl)
        {
		}
		protected override void DrawTabCloser(int index, Graphics graphics){
            if (this._ShowTabCloser && !(this._TabControl.TabPages[index] is INonCloseableTabPage))
                base.DrawTabCloser(index, graphics);
		}	
	}
}
