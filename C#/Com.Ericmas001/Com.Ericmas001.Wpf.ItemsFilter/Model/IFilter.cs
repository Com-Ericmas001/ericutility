using Com.Ericmas001.Wpf.ItemsFilter.ViewModel;
using System;
using System.ComponentModel;

namespace Com.Ericmas001.Wpf.ItemsFilter.Model
{
    /// <summary>
    /// Defines the contract for a filter used by the FilteredCollection
    /// </summary>
    public interface IFilter {
        /// <summary>
        /// Get or set value that indicates are filter include in filter function.
        /// </summary>
        bool IsActive {
            get;
            set;
        }

        /// <summary>
        /// Determines whether the specified target is matching the criteria.
        /// </summary>
        void IsMatch(FilterPresenter sender, FilterEventArgs e);
    }
    
}
