using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Com.Ericmas001.AppMonitor.DataTypes.DataElements;
using Com.Ericmas001.Portable.Util.Entities;
using Com.Ericmas001.Wpf.ViewModels.Trees;

namespace Com.Ericmas001.AppMonitor.DataTypes.TreeElements
{
    public class BaseLeafTreeElement : TreeElementViewModel, IBaseTreeElement
    {
        private readonly IEnumerable<string> m_UsedStringCriterias;

        public IEnumerable<string> UsedStringCriterias
        {
            get { return m_UsedStringCriterias; }
        }

        private ObservableCollection<BaseDataElement> m_Tabs;
        private BaseDataElement m_SelectedTab;
        private readonly IDataItem m_DataItem;
        public BaseDataElement SelectedTab
        {
            get { return m_SelectedTab; }
            set
            {
                m_SelectedTab = value;
                RaisePropertyChanged("SelectedTab");
            }
        }
        public ObservableCollection<BaseDataElement> Tabs
        {
            get
            {
                if (m_Tabs == null)
                {
                    m_Tabs = new ObservableCollection<BaseDataElement>();
                    m_Tabs.CollectionChanged += delegate
                    {
                        if (m_Tabs.Any())
                        {
                            SelectedTab = m_Tabs.First();
                        }
                    };
                    InitDataItem();
                    SetTabs().ToList().ForEach(x => m_Tabs.Add(x));
                    RaisePropertyChanged("HasOnlyOneDataTab");
                }
                return m_Tabs;
            }
        }
        public override string Text
        {
            get { return m_DataItem.ToString(); }
        }

        public IDataItem DataItem
        {
            get { return m_DataItem; }
        }
        public bool HasOnlyOneDataTab
        {
            get { return Tabs.Count == 1; }
        }
        public BaseDataElement FirstDataTab
        {
            get { return Tabs.FirstOrDefault(); }
        }

        public BaseLeafTreeElement(TreeElementViewModel parent, IEnumerable<string> usedCriterias, IDataItem dataItem)
            : base(parent)
        {
            m_DataItem = dataItem;
            m_UsedStringCriterias = usedCriterias;
        }
        protected virtual IEnumerable<BaseDataElement> SetTabs()
        {
            return new BaseDataElement[] { new RawDataElement(DataItem) };
        }
        protected virtual void InitDataItem()
        {
        }
    }
}
