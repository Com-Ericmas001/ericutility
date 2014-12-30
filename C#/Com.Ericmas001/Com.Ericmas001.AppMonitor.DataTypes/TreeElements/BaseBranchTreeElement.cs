using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using Com.Ericmas001.AppMonitor.DataTypes.GlobalElements;
using Com.Ericmas001.Util;
using Com.Ericmas001.Wpf.ViewModels.Trees;

namespace Com.Ericmas001.AppMonitor.DataTypes.TreeElements
{
    public class BaseBranchTreeElement<TCategory, TCriteria> : BaseTreeElement<TCategory, TCriteria>
        where TCategory : struct
        where TCriteria : struct
    {

        private ObservableCollection<BaseGlobalElement<TCategory, TCriteria>> m_Tabs;
        private BaseGlobalElement<TCategory, TCriteria> m_SelectedTab;
        public override string Text
        {
            get { return String.Format("{0} ({1})", BranchName, TreeLeaves.Length); }
        }

        protected virtual string BranchName
        {
            get
            {
                if (TreeLeaves.Any())
                    return TreeLeaves.First().DataItem.ObtainValue(EnumFactory<TCriteria>.ToString(SearchCriteria));
                return String.Format("No Result for {0}", EnumFactory<TCriteria>.ToString(SearchCriteria)); 
            }
        }
        public BaseGlobalElement<TCategory, TCriteria> SelectedTab
        {
            get { return m_SelectedTab; }
            set
            {
                m_SelectedTab = value;
                RaisePropertyChanged("SelectedTab");
            }
        }
        public ObservableCollection<BaseGlobalElement<TCategory, TCriteria>> Tabs
        {
            get
            {
                if (m_Tabs == null)
                {
                    m_Tabs = new ObservableCollection<BaseGlobalElement<TCategory, TCriteria>>();
                    m_Tabs.CollectionChanged += delegate
                    {
                        if (m_Tabs.Any())
                            SelectedTab = m_Tabs.First();
                    };
                    SetTabs().ToList().ForEach(x => m_Tabs.Add(x));
                    RaisePropertyChanged("HasOnlyOneGlobalTab");
                }
                return m_Tabs;
            }
        }
        public override FontStyle FontStyle
        {
            get
            {
                IGrouping<FontStyle, BaseLeafTreeElement<TCategory, TCriteria>>[] differents = TreeLeaves.GroupBy(x => x.FontStyle).ToArray();
                if (differents.Count() == 1)
                {
                    return differents.First().First().FontStyle;
                }
                return base.FontStyle;
            }
        }
        public override FontFamily FontFamily
        {
            get
            {
                IGrouping<FontStyle, BaseLeafTreeElement<TCategory, TCriteria>>[] differents = TreeLeaves.GroupBy(x => x.FontStyle).ToArray();
                if (differents.Count() == 1)
                {
                    return differents.First().First().FontFamily;
                }
                return base.FontFamily;
            }
        }
        public override FontWeight FontWeight
        {
            get
            {
                IGrouping<FontStyle, BaseLeafTreeElement<TCategory, TCriteria>>[] differents = TreeLeaves.GroupBy(x => x.FontStyle).ToArray();
                if (differents.Count() == 1)
                {
                    return differents.First().First().FontWeight;
                }
                return base.FontWeight;
            }
        }
        protected virtual IEnumerable<BaseGlobalElement<TCategory, TCriteria>> SetTabs()
{
	return new BaseGlobalElement<TCategory, TCriteria>[0];
}
        public bool HasOnlyOneGlobalTab
        {
            get { return Tabs.Count == 1; }
        }
        public BaseGlobalElement<TCategory, TCriteria> FirstGlobalTab
        {
            get { return Tabs.FirstOrDefault(); }
        }

        public new BaseLeafTreeElement<TCategory, TCriteria>[] TreeLeaves
        {
            get { return base.TreeLeaves.OfType<BaseLeafTreeElement<TCategory, TCriteria>>().ToArray(); }
        }

        public BaseBranchTreeElement(TreeElementViewModel parent, IEnumerable<TCriteria> usedCriterias, TCriteria searchCriteria, TCategory category)
            : base(parent, usedCriterias, searchCriteria, category)
        {
        }
    }
}
