using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Com.Ericmas001.AppMonitor.DataTypes.Attributes;
using Com.Ericmas001.AppMonitor.DataTypes.Helpers;
using Com.Ericmas001.Wpf;
using Com.Ericmas001.Wpf.Entities.Enums;
using Com.Ericmas001.Wpf.ViewModels.SearchElements;

namespace Com.Ericmas001.AppMonitor.DataTypes.ViewModels.Sections
{
    public abstract class ActionButtonSection : TabSection
    {
        private RelayCommand m_ExecuteCommand;
        public ICommand ExecuteCommand
        {
            get { return m_ExecuteCommand ?? (m_ExecuteCommand = new RelayCommand(x => CreateNewTab(CreateContentTab()))); }
        }
    }
}
