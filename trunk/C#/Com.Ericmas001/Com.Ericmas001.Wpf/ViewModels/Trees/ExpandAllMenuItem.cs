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
    public class ExpandAllMenuItem :TreeContextMenuItemViewModel
    {
        public override string Text
        {
            get { return "Expand All"; }
        }
        public override string IconImageName
        {
            get { return "ExpandAll"; }
        }

        public ExpandAllMenuItem(TreeElementViewModel element) 
            : base(element)
        {
        }

        protected override void Execute()
        {
            Element.ExpandAll();
        }

        protected override bool CanExecute()
        {
            return Element.HasGrandChildren;
        }
    }
}
