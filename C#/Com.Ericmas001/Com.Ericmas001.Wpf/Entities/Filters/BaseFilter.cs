using System;
using System.Collections.Generic;
using System.Linq;
using Com.Ericmas001.Util.Entities;
using Com.Ericmas001.Util.Entities.Fields;
using Com.Ericmas001.Wpf.Entities.Filters.Commands;
using Com.Ericmas001.Wpf.Entities.Filters.Comparators;
using Com.Ericmas001.Wpf.ViewModels;

namespace Com.Ericmas001.Wpf.Entities.Filters
{
    public abstract class BaseFilter : BaseViewModel
    {

        public event EventHandler AddMeAsAFilter;
        public event EventHandler RemoveMeAsAFilter;
        public event EventHandler UpdateAFilter;

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

        private bool m_IsActive = true;

        public CheckListItem CurrentValueList
        {
            get { return m_CurrentValueList; }
            set
            {
                m_CurrentValueList = value;
                RaisePropertyChanged("CurrentValueList");
            }
        }

        public string CurrentValueString
        {
            get { return m_CurrentValueString; }
            set
            {
                m_CurrentValueString = value;
                RaisePropertyChanged("CurrentValueString");
            }
        }

        public string CurrentValueStringPair1
        {
            get { return m_CurrentValueStringPair1; }
            set
            {
                m_CurrentValueStringPair1 = value;
                RaisePropertyChanged("CurrentValueStringPair1");
            }
        }

        public string CurrentValueStringPair2
        {
            get { return m_CurrentValueStringPair2; }
            set
            {
                m_CurrentValueStringPair2 = value;
                RaisePropertyChanged("CurrentValueStringPair2");
            }
        }

        public DateTime CurrentValueDate
        {
            get { return m_CurrentValueDate; }
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
                m_CurrentFieldType = value;
                AvailablesItems = GenerateAvailablesItems();
                RaisePropertyChanged("CurrentFieldType");
            }
        }

        public CheckListItem[] AvailablesItems
        {
            get { return m_AvailablesItems; }
            set
            {
                m_AvailablesItems = value;
                RaisePropertyChanged("AvailablesItems");
            }
        }

        public bool IsActive
        {
            get { return m_IsActive; }
            set
            {
                if (value != m_IsActive)
                {
                    m_IsActive = value;
                    if (UpdateAFilter != null)
                        UpdateAFilter(this, new EventArgs());
                    RaisePropertyChanged("IsActive");
                }
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
            get { return m_AvailablesComparators.Length == 1; }
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
            get { return m_AvailablesCommands.Length == 1; }
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

        private RelayCommand m_DeleteCommand;

        public RelayCommand DeleteCommand
        {
            get { return m_DeleteCommand ?? (m_DeleteCommand = new RelayCommand(x => RemoveFilter())); }
        }

        private void AddFilter()
        {
            if (AddMeAsAFilter != null)
                AddMeAsAFilter(this, new EventArgs());
        }

        private void RemoveFilter()
        {
            if (RemoveMeAsAFilter != null)
                RemoveMeAsAFilter(this, new EventArgs());
        }

        protected abstract IEnumerable<IFilterCommand> GetAllCommands();
        protected abstract IEnumerable<IFilterComparator> GetAllComparators(); 


        public BaseFilter(string field, IBunchOfDataItems dataItems)
        {
            m_Field = field;
            m_DataItems = dataItems;
        }

        private bool m_IsInit = false;
        private void Init()
        {
            if (!m_IsInit)
            {
                m_IsInit = true;
                m_AvailablesCommands = GetAllCommands().ToArray();
                m_CurrentCommand = m_AvailablesCommands.First();
                m_AvailablesComparators = GetAllComparators().ToArray();
                m_CurrentComparator = HasOnlyOneComparator ? m_AvailablesComparators.First() : null;
                CurrentFieldType = GenerateFieldType();
            }
        }

        protected abstract FieldTypeEnum GenerateFieldType();

        protected abstract CheckListItem[] GenerateAvailablesItems();

        public string Description
        {
            get { return ToString(); }
        }

        public override string ToString()
        {
            string values;
            switch (m_CurrentFieldType)
            {
                case FieldTypeEnum.CheckList:
                    values = "{" + string.Join(", ", m_AvailablesItems.Where(x => x.IsSelected).Select(x => x.Name)) + "}";
                    break;
                case FieldTypeEnum.List:
                    values = m_CurrentValueList.Name;
                    break;
                case FieldTypeEnum.Date:
                    values = m_CurrentValueDate.ToString("yyyy-MM-dd");
                    break;
                case FieldTypeEnum.IntPair:
                    values = String.Format("{0} And {1}", int.Parse(m_CurrentValueStringPair1), int.Parse(m_CurrentValueStringPair2));
                    break;
                default:
                    values = m_CurrentValueString;
                    break;
            }

            return string.Format("{0} {1} {2} {3}", m_Field, m_CurrentCommand.Description,m_CurrentComparator.Description, values);
        }

        public bool IsSurvivingTheFilter(string value, IDataItem item)
        {

            if (!m_IsActive)
            {
                return true;
            }

            return CheckIfIsSurvivingTheFilter(value, item);
        }

        protected abstract bool CheckIfIsSurvivingTheFilter(string value, IDataItem item);
    }
}
