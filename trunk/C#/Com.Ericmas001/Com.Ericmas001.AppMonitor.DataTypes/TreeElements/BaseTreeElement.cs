using System;
using System.Collections.Generic;
using Com.Ericmas001.Wpf.ViewModels.Tabs;
using Com.Ericmas001.Wpf.ViewModels.Trees;

namespace Com.Ericmas001.AppMonitor.DataTypes.TreeElements
{
    public abstract class BaseTreeElement<TCategory, TCriteria> : TreeElementViewModel
        where TCategory : struct 
        where TCriteria : struct 
    {
        private readonly IEnumerable<TCriteria> m_UsedCriterias;
        private TCriteria m_SearchCriteria;
        private TCategory m_Category;

        public IEnumerable<TCriteria> UsedCriterias
        {
            get { return m_UsedCriterias; }
        }

        protected TCriteria SearchCriteria
        {
            get { return m_SearchCriteria; }
        }

        protected TCategory Category
        {
            get { return m_Category; }
        }

        public BaseTreeElement(TreeElementViewModel parent, IEnumerable<TCriteria> usedCriterias, TCriteria searchCriteria, TCategory category)
            : base(parent)
        {

            if (!typeof(TCategory).IsEnum)
                throw new Exception("<TCategory> must be of Enum type (" + typeof(TCategory).Name + ")");

            if (!typeof(TCriteria).IsEnum)
                throw new Exception("<TCriteria> must be of Enum type (" + typeof(TCriteria).Name + ")");

            m_UsedCriterias = usedCriterias;
            m_SearchCriteria = searchCriteria;
            m_Category = category;
        }
    }
}
