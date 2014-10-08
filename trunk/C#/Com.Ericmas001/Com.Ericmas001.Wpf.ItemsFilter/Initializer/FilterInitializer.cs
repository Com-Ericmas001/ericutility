using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Com.Ericmas001.Wpf.ItemsFilter.Model;

namespace Com.Ericmas001.Wpf.ItemsFilter.Initializer
{
    /// <summary>
    /// Base class for filter initializer.
    /// </summary>
    public abstract class FilterInitializer  {
        /// <summary>
        /// Generate new instance of Filter class, if it is possible for filterPresenter and key.
        /// </summary>
        /// <param name="filterPresenter">FilterPresenter, which can be attached Filter</param>
        /// <param name="key">Key for generated Filter. For PropertyFilter, key used as the name for binding property in filterPresenter.Parent collection.</param>
        /// <returns>Instance of Filter class or null.</returns>
        public abstract Filter NewFilter(FilterPresenter filterPresenter, object key);
    }
   
}
