using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace EricUtility.Windows.Forms
{
    public partial class FixedCustomizedTabPage : CustomizedTabPage, INonCloseableTabPage
    {

        public FixedCustomizedTabPage(TabPageContent content) : base(content)
        {
        }
    }
}
