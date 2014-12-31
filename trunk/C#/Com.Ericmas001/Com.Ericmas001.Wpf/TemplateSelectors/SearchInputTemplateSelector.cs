using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Com.Ericmas001.Wpf.ViewModels.SearchElements;

namespace Com.Ericmas001.Wpf.TemplateSelectors
{
    public class SearchInputTemplateSelector : DataTemplateSelector
    {
        public DataTemplate TextTemplate { get; set; }
        public DataTemplate DateTemplate { get; set; }
        public DataTemplate ListTemplate { get; set; }
        public DataTemplate PairTemplate { get; set; }
        public override System.Windows.DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is TextSearchElement ||
                item is GuidSearchElement ||
                item is IntSearchElement)
                return TextTemplate;

            if (item is DateSearchElement)
                return DateTemplate;

            if (item is ListSearchElement)
                return ListTemplate;

            if (item is IntPairSearchElement)
                return PairTemplate;

            return null;
        }
    }
}
