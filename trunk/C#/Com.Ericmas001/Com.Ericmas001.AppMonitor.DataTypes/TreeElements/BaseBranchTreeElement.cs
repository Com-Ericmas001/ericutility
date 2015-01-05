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
    public class BaseBranchTreeElement : TreeElementViewModel, IBaseTreeElement
    {
        private readonly string m_SearchStringCriteria;
        private readonly IEnumerable<string> m_UsedStringCriterias;

        public string SearchStringCriteria
        {
            get { return m_SearchStringCriteria; }
        }

        public IEnumerable<string> UsedStringCriterias
        {
            get { return m_UsedStringCriterias; }
        }
        private ObservableCollection<BaseGlobalElement> m_Tabs;
        private BaseGlobalElement m_SelectedTab;
        public override string Text
        {
            get { return String.Format("{0} ({1})", BranchName, TreeLeaves.Length); }
        }

        protected virtual string BranchName
        {
            get
            {
                if (TreeLeaves.Any())
                    return TreeLeaves.First().DataItem.ObtainValue(SearchStringCriteria);
                return String.Format("No Result for {0}", SearchStringCriteria); 
            }
        }
        public BaseGlobalElement SelectedTab
        {
            get { return m_SelectedTab; }
            set
            {
                m_SelectedTab = value;
                RaisePropertyChanged("SelectedTab");
            }
        }
        public ObservableCollection<BaseGlobalElement> Tabs
        {
            get
            {
                if (m_Tabs == null)
                {
                    m_Tabs = new ObservableCollection<BaseGlobalElement>();
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
                IGrouping<FontStyle, BaseLeafTreeElement>[] differents = TreeLeaves.GroupBy(x => x.FontStyle).ToArray();
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
                IGrouping<FontStyle, BaseLeafTreeElement>[] differents = TreeLeaves.GroupBy(x => x.FontStyle).ToArray();
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
                IGrouping<FontStyle, BaseLeafTreeElement>[] differents = TreeLeaves.GroupBy(x => x.FontStyle).ToArray();
                if (differents.Count() == 1)
                {
                    return differents.First().First().FontWeight;
                }
                return base.FontWeight;
            }
        }
        protected virtual IEnumerable<BaseGlobalElement> SetTabs()
        {
	        return new BaseGlobalElement[0];
        }
        public bool HasOnlyOneGlobalTab
        {
            get { return Tabs.Count == 1; }
        }
        public BaseGlobalElement FirstGlobalTab
        {
            get { return Tabs.FirstOrDefault(); }
        }

        public new BaseLeafTreeElement[] TreeLeaves
        {
            get { return base.TreeLeaves.OfType<BaseLeafTreeElement>().ToArray(); }
        }

        public BaseBranchTreeElement(TreeElementViewModel parent, IEnumerable<string> usedCriterias, string searchCriteria)
            : base(parent)
        {
            m_UsedStringCriterias = usedCriterias;
            m_SearchStringCriteria = searchCriteria;
        }
    }
}
