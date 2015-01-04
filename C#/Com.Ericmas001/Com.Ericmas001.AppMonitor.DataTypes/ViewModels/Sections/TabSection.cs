﻿using System;
using System.Collections.Generic;
using System.Linq;
using Com.Ericmas001.AppMonitor.DataTypes.Attributes;
using Com.Ericmas001.AppMonitor.DataTypes.Entities;
using Com.Ericmas001.AppMonitor.DataTypes.Helpers;
using Com.Ericmas001.Wpf.Entities.Enums;
using Com.Ericmas001.Wpf.ViewModels.SearchElements;
using Com.Ericmas001.Wpf.ViewModels.Tabs;

namespace Com.Ericmas001.AppMonitor.DataTypes.ViewModels.Sections
{
    public abstract class TabSection : NewTabViewModel
    {
        public event EventHandler OnAfterExpanded = delegate { };

        private bool m_IsExpanded;

        public bool IsExpanded
        {
            get { return m_IsExpanded; }
            set
            {
                if (m_IsExpanded != value)
                {
                    m_IsExpanded = value;
                    RaisePropertyChanged("IsExpanded");
                    if (m_IsExpanded)
                        OnAfterExpanded(this, new EventArgs());
                }
            }
        }

        public TabInfo Info { get; protected set; }
    }
}
