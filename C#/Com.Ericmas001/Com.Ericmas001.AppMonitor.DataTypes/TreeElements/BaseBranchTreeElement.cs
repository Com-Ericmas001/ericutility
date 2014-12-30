using System;
using System.Collections.Generic;
using System.Linq;
using Com.Ericmas001.Util;
using Com.Ericmas001.Wpf.ViewModels.Trees;

namespace Com.Ericmas001.AppMonitor.DataTypes.TreeElements
{
    public class BaseBranchTreeElement<TCategory, TCriteria> : BaseTreeElement<TCategory, TCriteria>
        where TCategory : struct
        where TCriteria : struct 
    {
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
