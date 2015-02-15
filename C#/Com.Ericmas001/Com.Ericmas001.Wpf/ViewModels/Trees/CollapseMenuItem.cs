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
