using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Com.Ericmas001.Wpf.Entities.Filters;

namespace Com.Ericmas001.Wpf.ViewModels.Sections
{
    public class ChooseGroupViewModel : BaseViewModel
    {

        public event EventHandler OnGroupsChanged;

        private DisplayList<string> m_AvailablesGroups = new DisplayList<string>();
        private DisplayList<string> m_ChoosenGroups = new DisplayList<string>();
        private Dictionary<string, BaseFilter[]> m_FieldsToFilter = new Dictionary<string, BaseFilter[]>();
        private readonly ObservableCollection<BaseFilter> m_CurrentFilters = new ObservableCollection<BaseFilter>();
        private string m_CurrentField;
        private BaseFilter[] m_AvailablesFilters;
        private readonly Func<IEnumerable<string>, IEnumerable<string>> m_OrderByFunc;

        public ObservableCollection<BaseFilter> CurrentFilters
        {
            get { return m_CurrentFilters; }
        }

        public Dictionary<string, BaseFilter[]> FieldsToFilter
        {
            get { return m_FieldsToFilter; }
            set
            {
                m_FieldsToFilter = value;
                RaisePropertyChanged("FieldsToFilter");
                RaisePropertyChanged("AllFields");
            }
        }

        public string[] AllFields
        {
            get { return m_FieldsToFilter.Keys.ToArray(); }
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

        public BaseFilter[] AvailablesFilters
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

        private BaseFilter[] GenerateAvailableFilters()
        {
            return string.IsNullOrEmpty(CurrentField) ? new BaseFilter[0] : FieldsToFilter[CurrentField].Select(x => GenerateFilter(x)).ToArray();
        }

        public BaseFilter GenerateFilter(BaseFilter f)
        {
            f.AddMeAsAFilter += OnFilterAdded;
            f.RemoveMeAsAFilter += OnFilterRemoved;
            f.UpdateAFilter += OnFilterUpdated;
            return f;
        }

        private void OnFilterAdded(object sender, EventArgs e)
        {
            m_CurrentFilters.Add((BaseFilter) sender);
            CurrentField = null;
            OnFilterUpdated(sender, e);
        }

        private void OnFilterRemoved(object sender, EventArgs e)
        {
            m_CurrentFilters.Remove((BaseFilter) sender);
            OnFilterUpdated(sender, e);
        }

        private void OnFilterUpdated(object sender, EventArgs e)
        {
            if (OnGroupsChanged != null)
                OnGroupsChanged(sender, e);
        }

    }
}
