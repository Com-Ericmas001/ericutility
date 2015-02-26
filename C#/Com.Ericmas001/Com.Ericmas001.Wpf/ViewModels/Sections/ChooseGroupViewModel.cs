using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Com.Ericmas001.Util.Entities.Filters.Commands;
using Com.Ericmas001.Util.Entities.Filters.Comparators;
using Com.Ericmas001.Wpf.Entities;
using Com.Ericmas001.Wpf.Entities.Filters;

namespace Com.Ericmas001.Wpf.ViewModels.Sections
{
    public class ChooseGroupViewModel : BaseViewModel
    {

        public event EventHandler OnGroupsChanged;

        private DisplayList<string> m_AvailablesGroups = new DisplayList<string>();
        private DisplayList<string> m_ChoosenGroups = new DisplayList<string>();
        private Dictionary<string, BaseFilterInCreation[]> m_FieldsToFilter = new Dictionary<string, BaseFilterInCreation[]>();
        private readonly ObservableCollection<BaseCompiledFilter> m_CurrentFilters = new ObservableCollection<BaseCompiledFilter>();
        private string m_CurrentField;
        private BaseFilterInCreation[] m_AvailablesFilters;
        private readonly Func<IEnumerable<string>, IEnumerable<string>> m_OrderByFunc;

        public ObservableCollection<BaseCompiledFilter> CurrentFilters
        {
            get { return m_CurrentFilters; }
        }

        public IEnumerable<string> FieldsToFilter
        {
            get { return m_FieldsToFilter.Keys; }
        }

        public void AddFieldToFilter(string field, BaseFilterInCreation[] filters)
        {
            foreach (BaseFilterInCreation f in filters)
                RegisterToEvents(f);
            m_FieldsToFilter.Add(field, filters);
            RaisePropertyChanged("AllFields");

        }

        public void RemoveAllFieldsToFilter()
        {
            foreach(string k in FieldsToFilter)
                foreach (BaseFilterInCreation f in m_FieldsToFilter[k])
                    UnRegisterToEvents(f);
            m_FieldsToFilter.Clear();
            RaisePropertyChanged("AllFields");

        }

        public string[] AllFields
        {
            get { return FieldsToFilter.ToArray(); }
        }

        public DisplayList<string> AvailablesGroups
        {
            get { return m_AvailablesGroups; }
            set
            {
                m_AvailablesGroups = value;
                RaisePropertyChanged("AvailablesGroups");
            }
        }

        public DisplayList<string> ChoosenGroups
        {
            get { return m_ChoosenGroups; }
            set
            {
                m_ChoosenGroups = value;
                RaisePropertyChanged("ChoosenGroups");
            }
        }

        public string CurrentField
        {
            get { return m_CurrentField; }
            set
            {
                m_CurrentField = value;
                m_AvailablesFilters = GenerateAvailableFilters();
                RaisePropertyChanged("CurrentField");
                RaisePropertyChanged("AvailablesFilters");
            }
        }

        public BaseFilterInCreation[] AvailablesFilters
        {
            get { return m_AvailablesFilters; }
        }

        private RelayCommand m_ChooseCritereCommand;
        private RelayCommand m_RemoveCritereCommand;
        private RelayCommand m_MoveUpCritereCommand;
        private RelayCommand m_MoveDownCritereCommand;

        public ICommand ChooseCritereCommand
        {
            get { return m_ChooseCritereCommand ?? (m_ChooseCritereCommand = new RelayCommand(x => OnCritereChoosen(), x => CanChooseCritere())); }
        }

        public ICommand RemoveCritereCommand
        {
            get { return m_RemoveCritereCommand ?? (m_RemoveCritereCommand = new RelayCommand(x => OnCritereRemoved(), x => CanRemoveCritere())); }
        }

        public ICommand MoveUpCritereCommand
        {
            get { return m_MoveUpCritereCommand ?? (m_MoveUpCritereCommand = new RelayCommand(x => OnCritereMovedUp(), x => CanMoveUpCritere())); }
        }

        public ICommand MoveDownCritereCommand
        {
            get { return m_MoveDownCritereCommand ?? (m_MoveDownCritereCommand = new RelayCommand(x => OnCritereMovedDown(), x => CanMoveDownCritere())); }
        }

        public ChooseGroupViewModel(IEnumerable<string> availables, Func<IEnumerable<string>,IEnumerable<string>> orderBy, IEnumerable<string> alreadyGrouped = null)
        {
            m_OrderByFunc = orderBy;

            AvailablesGroups.Items = new FastObservableCollection<string>();
            ChoosenGroups.Items = new FastObservableCollection<string>();

            if (availables != null)
                availables.ToList().ForEach(x => AvailablesGroups.Items.Add(x));
            if (alreadyGrouped != null)
                alreadyGrouped.ToList().ForEach(x => ChoosenGroups.Items.Add(x));

        }

        private bool CanChooseCritere()
        {
            return AvailablesGroups.SelectedIndex >= 0;
        }

        private bool CanRemoveCritere()
        {
            return ChoosenGroups.SelectedIndex >= 0;
        }

        private bool CanMoveUpCritere()
        {
            return ChoosenGroups.SelectedIndex >= 1;
        }

        private bool CanMoveDownCritere()
        {
            return ChoosenGroups.SelectedIndex >= 0 && ChoosenGroups.SelectedIndex < (ChoosenGroups.Items.Count - 1);
        }

        public void ChooseGroupingCriteria(string group)
        {
            AvailablesGroups.Selected = group;
            OnCritereChoosen();
        }

        private void OnCritereChoosen()
        {
            ChoosenGroups.Items.Add(AvailablesGroups.Selected);
            AvailablesGroups.Items.Remove(AvailablesGroups.Selected);
            ChoosenGroups.Selected = AvailablesGroups.Selected;
            AvailablesGroups.Selected = null;
            if (OnGroupsChanged != null)
                OnGroupsChanged(this, new EventArgs());
        }

        private void OnCritereRemoved()
        {
            var values = AvailablesGroups.Items.ToList();
            values.Add(ChoosenGroups.Selected);
            AvailablesGroups.Items.Clear();
            AvailablesGroups.Items.AddItems(m_OrderByFunc(values).ToList());
            ChoosenGroups.Items.Remove(ChoosenGroups.Selected);
            AvailablesGroups.Selected = ChoosenGroups.Selected;
            ChoosenGroups.Selected = null;
            if (OnGroupsChanged != null)
                OnGroupsChanged(this, new EventArgs());
        }

        private void OnCritereMovedUp()
        {
            int newIndex = ChoosenGroups.SelectedIndex - 1;
            ChoosenGroups.Items.Move(ChoosenGroups.SelectedIndex, newIndex);
            ChoosenGroups.SelectedIndex = newIndex;
            if (OnGroupsChanged != null)
                OnGroupsChanged(this, new EventArgs());
        }

        private void OnCritereMovedDown()
        {
            int newIndex = ChoosenGroups.SelectedIndex + 1;
            ChoosenGroups.Items.Move(ChoosenGroups.SelectedIndex, newIndex);
            ChoosenGroups.SelectedIndex = newIndex;
            if (OnGroupsChanged != null)
                OnGroupsChanged(this, new EventArgs());
        }

        private BaseFilterInCreation[] GenerateAvailableFilters()
        {
            return string.IsNullOrEmpty(CurrentField) ? new BaseFilterInCreation[0] : m_FieldsToFilter[CurrentField].ToArray();
        }

        private void RegisterToEvents(BaseFilterInCreation f)
        {
            f.AddMeAsAFilter += OnFilterAdded;
        }
        private void UnRegisterToEvents(BaseFilterInCreation f)
        {
            f.AddMeAsAFilter -= OnFilterAdded;
        }
        private void RegisterToEvents(BaseCompiledFilter f)
        {
            f.RemoveMeAsAFilter += OnFilterRemoved;
            f.UpdateAFilter += OnFilterUpdated;
        }
        private void UnRegisterToEvents(BaseCompiledFilter f)
        {
            f.RemoveMeAsAFilter -= OnFilterRemoved;
            f.UpdateAFilter -= OnFilterUpdated;
        }

        private void OnFilterAdded(object sender, EventArgs e)
        {
            AddCompiledFilter((BaseCompiledFilter) sender);
            CurrentField = null;
            OnFilterUpdated(sender, e);
        }

        private void OnFilterRemoved(object sender, EventArgs e)
        {
            m_CurrentFilters.Remove((BaseCompiledFilter)sender);
            OnFilterUpdated(sender, e);
        }

        private void OnFilterUpdated(object sender, EventArgs e)
        {
            if (OnGroupsChanged != null)
                OnGroupsChanged(sender, e);
        }
        private void AddFilter(BaseFilterInCreation f)
        {
            RegisterToEvents(f);
            f.AddCommand.Execute(null);
        }

        public BaseFilterInCreation AddCheckListFilter(BaseFilterInCreation f, IFilterCommand command, IFilterComparator comparator, Func<CheckListItem, bool> selectionFunc)
        {
            f.CurrentCommand = command;
            f.CurrentComparator = comparator;
            f.AvailablesItems.Where(selectionFunc).ToList().ForEach(x => x.IsSelected = true);
            AddFilter(f);
            return f;
        }
        public BaseFilterInCreation AddSingleListFilter(BaseFilterInCreation f, IFilterCommand command, IFilterComparator comparator, Func<CheckListItem, bool> selectionFunc)
        {
            f.CurrentCommand = command;
            f.CurrentComparator = comparator;
            f.CurrentValueList = f.AvailablesItems.Single(selectionFunc);
            AddFilter(f);
            return f;
        }
        public BaseFilterInCreation AddStringPairFilter(BaseFilterInCreation f, IFilterCommand command, IFilterComparator comparator, string value1, string value2)
        {
            f.CurrentCommand = command;
            f.CurrentComparator = comparator;
            f.CurrentValueStringPair1 = value1;
            f.CurrentValueStringPair2 = value2;
            AddFilter(f);
            return f;
        }
        public BaseFilterInCreation AddStringFilter(BaseFilterInCreation f, IFilterCommand command, IFilterComparator comparator, string value)
        {
            f.CurrentCommand = command;
            f.CurrentComparator = comparator;
            f.CurrentValueString = value;
            AddFilter(f);
            return f;
        }
        public BaseFilterInCreation AddDateFilter(BaseFilterInCreation f, IFilterCommand command, IFilterComparator comparator, DateTime value)
        {
            f.CurrentCommand = command;
            f.CurrentComparator = comparator;
            f.CurrentValueDate = value;
            AddFilter(f);
            return f;
        }

        public void AddCompiledFilter(BaseCompiledFilter cf)
        {
            RegisterToEvents(cf);
            m_CurrentFilters.Add(cf);
        }
    }
}
