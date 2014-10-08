using Com.Ericmas001.Wpf.ItemsFilter.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Com.Ericmas001.Wpf.ItemsFilter.Initializer
{
    /// <summary>
    /// Base class for PropertyFilter initialiser.
    /// </summary>
    public abstract class PropertyFilterInitializer:FilterInitializer
    {
        /// <summary>
        /// Generate new instance of Filter class, if it is possible for filterPresenter and key.
        /// </summary>
        /// <param name="filterPresenter">FilterPresenter, which can be attached Filter</param>
        /// <param name="key">Key for generated Filter. For PropertyFilter, key used as the name for binding property in filterPresenter.Parent collection.</param>
        /// <returns>Instance of Filter class or null.</returns>
        public sealed override Filter NewFilter(FilterPresenter filterPresenter, object key)
        {
            Debug.Assert(filterPresenter != null);
            Debug.Assert(key != null);

            if (key is ItemPropertyInfo)
                return NewFilter(filterPresenter, (ItemPropertyInfo)key);
            if (key is string)
            {
                ItemPropertyInfo propertyInfo = filterPresenter.ItemProperties.FirstOrDefault(item => item.Name == (string)key);
                if (propertyInfo != null)
                {
                    return NewFilter(filterPresenter, propertyInfo);
                }
            }
            return null;
        }
        /// <summary>
        /// Create instance of PropertyFilter for  filterPresenter and key, if it is possible.
        /// </summary>
        protected abstract PropertyFilter NewFilter(FilterPresenter filterPresenter, ItemPropertyInfo key);
    }
}
