using System;
using System.Linq;
using Com.Ericmas001.AppMonitor.DataTypes.Attributes;
using Com.Ericmas001.AppMonitor.DataTypes.Enums;
using Com.Ericmas001.Util;
using Com.Ericmas001.Wpf;
using Com.Ericmas001.Wpf.Entities;
using Com.Ericmas001.Wpf.Entities.Attributes;
using Com.Ericmas001.Wpf.Entities.Enums;
using Com.Ericmas001.Wpf.ViewModels;

namespace Com.Ericmas001.AppMonitor.DataTypes.Entities
{
    public class Filter : BaseViewModel
    {

        public event EventHandler AddMeAsAFilter;
        public event EventHandler RemoveMeAsAFilter;
        public event EventHandler UpdateAFilter;

        private readonly string m_Field;
        private readonly FilterEnum m_FilterType;
        private readonly FilterCommandEnum[] m_AvailablesCommands;

        private readonly FilterComparatorEnum[] m_AvailablesComparators;
        private readonly IBunchOfDataItems m_DataItems;
        private CheckListItem[] m_AvailablesItems;
        private FilterCommandEnum m_CurrentCommand;
        private FilterComparatorEnum m_CurrentComparator;
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

        public FilterCommandEnum CurrentCommand
        {
            get { return m_CurrentCommand; }
            set
            {
                m_CurrentCommand = value;
                RaisePropertyChanged("CurrentCommand");
            }
        }

        public FilterComparatorEnum CurrentComparator
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

        public FilterComparatorEnum[] AvailablesComparators
        {
            get { return m_AvailablesComparators; }
        }

        public bool HasOnlyOneComparator
        {
            //None + the one
            get { return m_AvailablesComparators.Length == 2; }
        }

        public FilterCommandEnum[] AvailablesCommands
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
            m_AvailablesCommands = EnumFactory<FilterEnum>.GetAttribute<FilterCommandAttribute>(filterType).Commands.ToArray();
            m_CurrentCommand = m_AvailablesCommands.First();
            m_AvailablesComparators = new[] {FilterComparatorEnum.None}.Union(EnumFactory<FilterEnum>.GetAttribute<FilterComparatorAttribute>(filterType).Comparators).ToArray();
            m_CurrentComparator = HasOnlyOneComparator ? m_AvailablesComparators.Last() : m_AvailablesComparators.First();
            CurrentSearchType = GenerateSearchType();
        }

        private SearchTypeEnum GenerateSearchType()
        {
            dynamic compAtt = EnumFactory<FilterComparatorEnum>.GetAttribute<SearchTypeAttribute>(CurrentComparator);
            if ((compAtt != null))
            {
                return compAtt.SearchType;
            }
            dynamic filterAtt = EnumFactory<FilterEnum>.GetAttribute<SearchTypeAttribute>(FilterType);
            if ((filterAtt != null))
            {
                return filterAtt.SearchType;
            }
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

            return string.Format("{0} {1} {2} {3}", m_Field, EnumFactory<FilterCommandEnum>.ToString(m_CurrentCommand), EnumFactory<FilterComparatorEnum>.ToString(m_CurrentComparator), values);
        }

        public bool IsSurvivingTheFilter(string value)
        {

            if (!m_IsActive)
            {
                return true;
            }

            switch (m_FilterType)
            {

                case FilterEnum.Text:
                    switch (m_CurrentComparator)
                    {
                        case FilterComparatorEnum.TextEqual:
                            return m_AvailablesItems.Where(x => x.IsSelected).Select(x => (string) x.Value).ToArray().Contains(value) == (m_CurrentCommand == FilterCommandEnum.Must);
                        case FilterComparatorEnum.Contains:
                            return value.Contains(m_CurrentValueString) == (m_CurrentCommand == FilterCommandEnum.Must);
                        case FilterComparatorEnum.StartsWith:
                            return value.StartsWith(m_CurrentValueString) == (m_CurrentCommand == FilterCommandEnum.Must);
                        case FilterComparatorEnum.EndsWith:
                            return value.EndsWith(m_CurrentValueString) == (m_CurrentCommand == FilterCommandEnum.Must);
                    }

                    break;
                case FilterEnum.Int:
                    bool answer = false;
                    int myNum = int.Parse(value);
                    if (m_CurrentComparator == FilterComparatorEnum.IntBetween)
                    {
                        int val1 = int.Parse(m_CurrentValueStringPair1);
                        int val2 = int.Parse(m_CurrentValueStringPair2);
                        answer = (myNum >= val1 && myNum <= val2);
                    }
                    else
                    {
                        int valNum = int.Parse(m_CurrentValueString);
                        switch (m_CurrentComparator)
                        {
                            case FilterComparatorEnum.SmallerThan:
                                answer = myNum < valNum;
                                break;
                            case FilterComparatorEnum.SmallerEqual:
                                answer = myNum <= valNum;
                                break;
                            case FilterComparatorEnum.IntEqual:
                                answer = myNum == valNum;
                                break;
                            case FilterComparatorEnum.IntNotEqual:
                                answer = myNum != valNum;
                                break;
                            case FilterComparatorEnum.GreaterEqual:
                                answer = myNum >= valNum;
                                break;
                            case FilterComparatorEnum.GreaterThan:
                                answer = myNum > valNum;
                                break;
                        }
                    }
                    return answer == (m_CurrentCommand == FilterCommandEnum.Must);
                case FilterEnum.Date:
                case FilterEnum.Time:
                    string validation = m_FilterType == FilterEnum.Date ? m_CurrentValueDate.ToString("yyyy-MM-dd") : m_CurrentValueString;
                    switch (m_CurrentComparator)
                    {
                        case FilterComparatorEnum.SmallerThan:
                            return String.Compare(value, validation, StringComparison.Ordinal) < 0;
                        case FilterComparatorEnum.SmallerEqual:
                            return String.Compare(value, validation, StringComparison.Ordinal) <= 0;
                        case FilterComparatorEnum.IntEqual:
                            return value == validation;
                        case FilterComparatorEnum.IntNotEqual:
                            return value != validation;
                        case FilterComparatorEnum.GreaterEqual:
                            return String.Compare(value, validation, StringComparison.Ordinal) >= 0;
                        case FilterComparatorEnum.GreaterThan:
                            return String.Compare(value, validation, StringComparison.Ordinal) > 0;
                    }

                    break;
            }

            return true;
        }
    }
}
