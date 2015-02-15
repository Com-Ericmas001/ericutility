using System;
using System.Collections.Generic;
using System.Linq;
using Com.Ericmas001.Util.Entities;
using Com.Ericmas001.Util.Entities.Fields;
using Com.Ericmas001.Util.Entities.Filters.Commands;
using Com.Ericmas001.Util.Entities.Filters.Comparators;
using Com.Ericmas001.Wpf.ViewModels;

namespace Com.Ericmas001.Wpf.Entities.Filters
{
    public abstract class BaseFilterInCreation : BaseViewModel
    {
        public event EventHandler AddMeAsAFilter;

        private readonly string m_Field;
        private IFilterCommand[] m_AvailablesCommands;
        private IFilterComparator[] m_AvailablesComparators;
        private readonly IBunchOfDataItems m_DataItems;
        private CheckListItem[] m_AvailablesItems;
        private IFilterCommand m_CurrentCommand;
        private IFilterComparator m_CurrentComparator;
        private FieldTypeEnum m_CurrentFieldType;
        private string m_CurrentValueString;
        private string m_CurrentValueStringPair1;
        private string m_CurrentValueStringPair2;
        private DateTime m_CurrentValueDate = DateTime.Now;
        private CheckListItem m_CurrentValueList;

        public CheckListItem CurrentValueList
        {
            get
            {
                Init();
                return m_CurrentValueList;
            }
            set
            {
                m_CurrentValueList = value;
                RaisePropertyChanged("CurrentValueList");
            }
        }

        public string CurrentValueString
        {
            get
            {
                Init();
                return m_CurrentValueString;
            }
            set
            {
                m_CurrentValueString = value;
                RaisePropertyChanged("CurrentValueString");
            }
        }

        public string CurrentValueStringPair1
        {
            get
            {
                Init(); 
                return m_CurrentValueStringPair1;
            }
            set
            {
                m_CurrentValueStringPair1 = value;
                RaisePropertyChanged("CurrentValueStringPair1");
            }
        }

        public string CurrentValueStringPair2
        {
            get
            {
                Init(); 
                return m_CurrentValueStringPair2;
            }
            set
            {
                m_CurrentValueStringPair2 = value;
                RaisePropertyChanged("CurrentValueStringPair2");
            }
        }

        public DateTime CurrentValueDate
        {
            get
            {
                Init(); 
                return m_CurrentValueDate;
            }
            set
            {
                m_CurrentValueDate = value;
                RaisePropertyChanged("CurrentValueDate");
            }
        }

        public IFilterCommand CurrentCommand
        {
            get
            {
                Init(); 
                return m_CurrentCommand;
            }
            set
            {
                Init(); 
                m_CurrentCommand = value;
                RaisePropertyChanged("CurrentCommand");
            }
        }

        public IFilterComparator CurrentComparator
        {
            get
            {
                Init(); 
                return m_CurrentComparator;
            }
            set
            {
                Init(); 
                m_CurrentComparator = value;
                CurrentFieldType = GenerateFieldType();
                RaisePropertyChanged("CurrentComparator");
            }
        }

        public FieldTypeEnum CurrentFieldType
        {
            get
            {
                Init();
                return m_CurrentFieldType;
            }
            set
            {
                Init(); 
                m_CurrentFieldType = value;
                AvailablesItems = GenerateAvailablesItems();
                RaisePropertyChanged("CurrentFieldType");
            }
        }

        public CheckListItem[] AvailablesItems
        {
            get
            {
                Init(); 
                return m_AvailablesItems;
            }
            set
            {
                Init(); 
                m_AvailablesItems = value;
                RaisePropertyChanged("AvailablesItems");
            }
        }

        protected IBunchOfDataItems DataItems
        {
            get { return m_DataItems; }
        } 

        public IFilterComparator[] AvailablesComparators
        {
            get
            {
                Init();
                return m_AvailablesComparators;
            }
        }

        public bool HasOnlyOneComparator
        {
            get { return AvailablesComparators.Length == 1; }
        }

        public IFilterCommand[] AvailablesCommands
        {
            get
            {
                Init();
                return m_AvailablesCommands;
            }
        }

        public bool HasOnlyOneCommand
        {
            get { return AvailablesCommands.Length == 1; }
        }

        public string Field
        {
            get { return m_Field; }
        }

        private RelayCommand m_AddCommand;

        public RelayCommand AddCommand
        {
            get { return m_AddCommand ?? (m_AddCommand = new RelayCommand(x => AddFilter())); }
        }

        protected abstract BaseCompiledFilter CompileFilter();  
        private void AddFilter()
        {
            if (AddMeAsAFilter != null)
                AddMeAsAFilter(CompileFilter(), new EventArgs());
        }

        protected abstract IEnumerable<IFilterCommand> GetAllCommands();
        protected abstract IEnumerable<IFilterComparator> GetAllComparators(); 


        public BaseFilterInCreation(string field, IBunchOfDataItems dataItems)
        {
            m_Field = field;
            m_DataItems = dataItems;
        }

        private bool m_IsInit;
        private void Init()
        {
            if (!m_IsInit)
            {
                m_IsInit = true;
                m_AvailablesCommands = GetAllCommands().ToArray();
                m_CurrentCommand = m_AvailablesCommands.First();
                m_AvailablesComparators = GetAllComparators().ToArray();
                m_CurrentComparator = HasOnlyOneComparator ? AvailablesComparators.First() : null;
                CurrentFieldType = GenerateFieldType();
            }
        }

        protected abstract FieldTypeEnum GenerateFieldType();

        protected abstract CheckListItem[] GenerateAvailablesItems();
    }
}
