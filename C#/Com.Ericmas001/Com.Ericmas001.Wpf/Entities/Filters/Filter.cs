using System;
using System.Linq;
using Com.Ericmas001.Util;
using Com.Ericmas001.Util.Entities;
using Com.Ericmas001.Wpf.Entities.Attributes;
using Com.Ericmas001.Wpf.Entities.Enums;
using Com.Ericmas001.Wpf.Entities.Filters.Attributes;
using Com.Ericmas001.Wpf.Entities.Filters.Commands;
using Com.Ericmas001.Wpf.Entities.Filters.Comparators;
using Com.Ericmas001.Wpf.Entities.Filters.Enums;
using Com.Ericmas001.Wpf.ViewModels;

namespace Com.Ericmas001.Wpf.Entities.Filters
{
    public class Filter : BaseViewModel
    {

        public event EventHandler AddMeAsAFilter;
        public event EventHandler RemoveMeAsAFilter;
        public event EventHandler UpdateAFilter;

        private readonly string m_Field;
        private readonly FilterEnum m_FilterType;
        private readonly IFilterCommand[] m_AvailablesCommands;
        private readonly IFilterComparator[] m_AvailablesComparators;
        private readonly IBunchOfDataItems m_DataItems;
        private CheckListItem[] m_AvailablesItems;
        private IFilterCommand m_CurrentCommand;
        private IFilterComparator m_CurrentComparator;
        private SearchTypeEnum m_CurrentSearchType;
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
            get { return m_CurrentCommand; }
            set
            {
                m_CurrentCommand = value;
                RaisePropertyChanged("CurrentCommand");
            }
        }

        public IFilterComparator CurrentComparator
        {
            get { return m_CurrentComparator; }
            set
            {
                m_CurrentComparator = value;
                CurrentSearchType = GenerateSearchType();
                RaisePropertyChanged("CurrentComparator");
            }
        }

        public SearchTypeEnum CurrentSearchType
        {
            get { return m_CurrentSearchType; }
            set
            {
                m_CurrentSearchType = value;
                AvailablesItems = GenerateAvailablesItems();
                RaisePropertyChanged("CurrentSearchType");
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

        public IFilterComparator[] AvailablesComparators
        {
            get { return m_AvailablesComparators; }
        }

        public bool HasOnlyOneComparator
        {
            get { return m_AvailablesComparators.Length == 1; }
        }

        public IFilterCommand[] AvailablesCommands
        {
            get { return m_AvailablesCommands; }
        }

        public bool HasOnlyOneCommand
        {
            get { return m_AvailablesCommands.Length == 1; }
        }

        public FilterEnum FilterType
        {
            get { return m_FilterType; }
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

        public Filter(string field, FilterEnum filterType, IBunchOfDataItems dataItems)
        {
            m_Field = field;
            m_FilterType = filterType;
            m_DataItems = dataItems;
            m_AvailablesCommands = BasicFilterCommand.AllCommands(EnumFactory<FilterEnum>.GetAttribute<FilterCommandAttribute>(filterType).Commands.ToArray()).ToArray();
            m_CurrentCommand = m_AvailablesCommands.First();
            m_AvailablesComparators = BasicFilterComparator.AllComparators(EnumFactory<FilterEnum>.GetAttribute<FilterComparatorAttribute>(filterType).Comparators.ToArray()).ToArray();
            m_CurrentComparator = HasOnlyOneComparator ? m_AvailablesComparators.First() : null;
            CurrentSearchType = GenerateSearchType();
        }

        private SearchTypeEnum GenerateSearchType()
        {
            //TODO: Unbound from Basic !!!
            var comp = CurrentComparator as BasicFilterComparator;
            if (comp != null)
            {
                var compAtt = comp.SearchTypeOverrideAttribute;
                if (compAtt != null)
                    return compAtt.SearchType;
            }

            var filterAtt = EnumFactory<FilterEnum>.GetAttribute<SearchTypeAttribute>(FilterType);
            if (filterAtt != null)
                return filterAtt.SearchType;

            return SearchTypeEnum.None;
        }

        private CheckListItem[] GenerateAvailablesItems()
        {
            if (m_CurrentSearchType != SearchTypeEnum.List && m_CurrentSearchType != SearchTypeEnum.CheckList)
                return new CheckListItem[0];

            return m_DataItems.ObtainAllValues(m_Field).Select(x => new CheckListItem(x, x)).ToArray();
        }

        public string Description
        {
            get { return ToString(); }
        }

        public override string ToString()
        {
            string values;
            switch (m_CurrentSearchType)
            {
                case SearchTypeEnum.CheckList:
                    values = "{" + string.Join(", ", m_AvailablesItems.Where(x => x.IsSelected).Select(x => x.Name)) + "}";
                    break;
                case SearchTypeEnum.List:
                    values = m_CurrentValueList.Name;
                    break;
                case SearchTypeEnum.Date:
                    values = m_CurrentValueDate.ToString("yyyy-MM-dd");
                    break;
                case SearchTypeEnum.IntPair:
                    values = String.Format("{0} And {1}", int.Parse(m_CurrentValueStringPair1), int.Parse(m_CurrentValueStringPair2));
                    break;
                default:
                    values = m_CurrentValueString;
                    break;
            }

            return string.Format("{0} {1} {2} {3}", m_Field, m_CurrentCommand.Description,m_CurrentComparator.Description, values);
        }

        public bool IsSurvivingTheFilter(string value)
        {

            if (!m_IsActive)
            {
                return true;
            }

            switch (m_FilterType)
            {
                //TODO: Unbound from Basic !!!
                case FilterEnum.Text:
                case FilterEnum.Blob:
                    if (m_CurrentComparator is TextEqualBasicFilterComparator)
                        return m_CurrentCommand.IsDataFiltered(m_CurrentComparator, m_AvailablesItems.Where(x => x.IsSelected).Select(x => (string)x.Value), value);
                    return m_CurrentCommand.IsDataFiltered(m_CurrentComparator, m_CurrentValueString, value);
                case FilterEnum.Int:
                    if (m_CurrentComparator is IntBetweenBasicFilterComparator)
                        return m_CurrentCommand.IsDataFiltered(m_CurrentComparator, new Tuple<int, int>(int.Parse(m_CurrentValueStringPair1), int.Parse(m_CurrentValueStringPair2)), int.Parse(value));
                    return m_CurrentCommand.IsDataFiltered(m_CurrentComparator, int.Parse(m_CurrentValueString), int.Parse(value));
                case FilterEnum.Date:
                case FilterEnum.Time:
                    return m_CurrentCommand.IsDataFiltered(m_CurrentComparator, m_FilterType == FilterEnum.Date ? m_CurrentValueDate.ToString("yyyy-MM-dd") : m_CurrentValueString, value);
            }

            return true;
        }
    }
}
