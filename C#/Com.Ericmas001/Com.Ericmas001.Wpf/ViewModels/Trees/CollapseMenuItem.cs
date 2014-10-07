using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Com.Ericmas001.Wpf.ViewModels.Trees
{
    public class CollapseMenuItem :TreeContextMenuItemViewModel
    {

        public override string Text
        {
            get { return "Collapse"; }
        }
        public override string IconImageName
        {
            get { return "Collapse"; }
        }

        public CollapseMenuItem(TreeElementViewModel element) 
            : base(element)
        {
        }

        protected override void Execute()
        {
            Element.Collapse();
        }

        protected override bool CanExecute()
        {
            return Element.CanCollapse;
        }
    }
}
