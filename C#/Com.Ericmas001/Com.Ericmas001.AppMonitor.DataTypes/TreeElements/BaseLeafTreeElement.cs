using System.Collections.Generic;
using Com.Ericmas001.AppMonitor.DataTypes.Entities;
using Com.Ericmas001.Wpf.ViewModels.Trees;

namespace Com.Ericmas001.AppMonitor.DataTypes.TreeElements
{
    public class BaseLeafTreeElement<TCategory, TCriteria> : BaseTreeElement<TCategory, TCriteria> 
        where TCategory : struct
        where TCriteria : struct 
    {
        private readonly IDataItem m_DataItem;
        public override string Text
        {
            get { return m_DataItem.ToString(); }
        }

        public IDataItem DataItem
        {
            get { return m_DataItem; }
        }

        public BaseLeafTreeElement(TreeElementViewModel parent, IEnumerable<TCriteria> usedCriterias, TCriteria searchCriteria, TCategory category, IDataItem dataItem)
            : base(parent, usedCriterias, searchCriteria, category)
        {
            m_DataItem = dataItem;
        }
    }
}
