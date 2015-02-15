namespace Com.Ericmas001.Wpf.ViewModels.Trees
{
    public class CollapseAllMenuItem :TreeContextMenuItemViewModel
    {
        public override string Text
        {
            get { return "Collapse All"; }
        }
        public override string IconImageName
        {
            get { return "CollapseAll"; }
        }

        public CollapseAllMenuItem(TreeElementViewModel element) 
            : base(element)
        {
        }

        protected override void Execute()
        {
            Element.CollapseAll();
        }

        protected override bool CanExecute()
        {
            return Element.HasGrandChildren;
        }
    }
}
