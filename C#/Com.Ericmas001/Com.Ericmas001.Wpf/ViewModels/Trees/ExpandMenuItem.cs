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
