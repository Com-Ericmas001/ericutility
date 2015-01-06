using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Com.Ericmas001.AppMonitor.DataTypes.DataElements;
using Com.Ericmas001.Util;
using Com.Ericmas001.Util.Entities;
using Com.Ericmas001.Wpf.ViewModels.Trees;

namespace Com.Ericmas001.AppMonitor.DataTypes.TreeElements
{
    public class BaseCategoryLeafTreeElement<TCategory, TCriteria> : BaseLeafTreeElement, IBaseCategoryTreeElement<TCategory, TCriteria>
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

        public TCriteria SearchCriteria
        {
            get { return m_SearchCriteria; }
        }

        public TCategory Category
        {
            get { return m_Category; }
        }

        public BaseCategoryLeafTreeElement(TreeElementViewModel parent, IEnumerable<TCriteria> usedCriterias, TCriteria searchCriteria, TCategory category, IDataItem dataItem)
            : base(parent, usedCriterias.Select(x => EnumFactory<TCriteria>.ToString(x)),dataItem)
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
