using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;

namespace Com.Ericmas001.Wpf.ItemsFilter.Model
{
    /// <summary>
    /// Specify the [ModelView] class that present model.
    /// </summary>
    public class ViewAttribute:Attribute
    {
        private readonly Type viewType;

        public Type ViewType {
            get { return viewType; }
        } 

        public ViewAttribute(Type ViewType = null) {
            Debug.Assert(ViewType == null || typeof(FrameworkElement).IsAssignableFrom(ViewType));
            viewType = ViewType;
        }

    }
}
