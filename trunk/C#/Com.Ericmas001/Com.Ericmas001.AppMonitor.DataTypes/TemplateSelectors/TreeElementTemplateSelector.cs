using System.Windows;
using System.Windows.Controls;
using Com.Ericmas001.Wpf.ViewModels.Trees;

namespace Com.Ericmas001.AppMonitor.DataTypes.TemplateSelectors
{
    public class TreeElementTemplateSelector : DataTemplateSelector
    {
        public DataTemplate BranchTemplate { get; set; }
        public DataTemplate LeafTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is TreeElementViewModel)
            {
                TreeElementViewModel elem = (TreeElementViewModel)item;
                if (elem.NbLeaves > 0 || elem.Parent == null)
                    return BranchTemplate;
                return LeafTemplate;
            }
            return null;
        }
    }
}
