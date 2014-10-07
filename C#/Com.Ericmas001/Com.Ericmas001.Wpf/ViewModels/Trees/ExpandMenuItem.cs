using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Com.Ericmas001.Wpf.ViewModels.Trees
{
    public class ExpandMenuItem :TreeContextMenuItemViewModel
    {
        public override string Text
        {
            get { return "Expand"; }
        }
        public override string IconImageName
        {
            get { return "Expand"; }
        }

        public ExpandMenuItem(TreeElementViewModel element) 
            : base(element)
        {
        }

        protected override void Execute()
        {
            Element.Expand();
        }

        protected override bool CanExecute()
        {
            return Element.CanExpand;
        }
    }
}
