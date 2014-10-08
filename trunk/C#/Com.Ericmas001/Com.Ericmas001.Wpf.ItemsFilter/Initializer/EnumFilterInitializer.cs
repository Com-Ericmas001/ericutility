﻿using Com.Ericmas001.Wpf.ItemsFilter.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Com.Ericmas001.Wpf.ItemsFilter.Initializer
{
    /// <summary>
    /// Filter initializer for EnumFilter.
    /// </summary>
    public class EnumFilterInitializer:PropertyFilterInitializer
    {
        /// <summary>
        /// Generate new instance of EnumFilter class, if it is possible for a pair of filterPresenter and propertyInfo.
        /// </summary>
        /// <param name="filterPresenter">FilterPresenter, which can be attached Filter</param>
        /// <param name="key">Key, used as the name for binding property in filterPresenter.Parent collection.</param>
        /// <returns>Instance of EnumFilter class or null.</returns>
        protected override PropertyFilter NewFilter(FilterPresenter filterPresenter, ItemPropertyInfo propertyInfo)
        {
            Debug.Assert(filterPresenter != null);
            Debug.Assert(propertyInfo != null);
            Type propertyType = propertyInfo.PropertyType;
            if (filterPresenter.ItemProperties.Contains(propertyInfo)
                && propertyType.IsEnum
                )
            {
                return (PropertyFilter)Activator.CreateInstance(typeof(EnumFilter<>).MakeGenericType(propertyInfo.PropertyType), propertyInfo);
            }
            return null;
        }
    }
}
