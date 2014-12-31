using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Ericmas001.AppMonitor.DataTypes.Attributes;
using Com.Ericmas001.AppMonitor.DataTypes.ViewModels.Sections;
using Com.Ericmas001.Util;
using Com.Ericmas001.Util.Attributes;
using Com.Ericmas001.Wpf.ViewModels.Tabs;

namespace Com.Ericmas001.AppMonitor.DataTypes.ViewModels
{
    public abstract class NewSearchViewModel<TCriteria, TCategory, TSearchAttribute> : NewTabViewModel
        where TCriteria : struct
        where TCategory : struct
        where TSearchAttribute : Attribute, ISearchCriteriaAttribute<TCategory>
    {

        public override BaseTabViewModel CreateContentTab()
        {
            return null;
        }

        public CategorySection<TCriteria, TCategory, TSearchAttribute>[] Categories { get; private set; }

        protected virtual IEnumerable<TCategory> ExcludeCategories(IEnumerable<TCategory> categories)
        {
            return categories;
        }

        public NewSearchViewModel()
        {
            Categories = ExcludeCategories(EnumFactory<TCategory>.AllValues).OrderBy(x => EnumFactory<TCategory>.GetAttribute<PriorityAttribute>(x).Priority).ThenBy(x => EnumFactory<TCategory>.ToString(x)).Select(CreateSection).ToArray();

            if (Categories.Any())
                Categories.First().IsExpanded = true;

        }

        protected abstract CategorySection<TCriteria, TCategory, TSearchAttribute> CreateCategprySection(TCategory cat);

        public CategorySection<TCriteria, TCategory, TSearchAttribute> CreateSection(TCategory cat)
        {
            var section = CreateCategprySection(cat);
            section.OnAfterExpanded += section_OnAfterExpanded;
            section.OnTabCreation += (sender, tab) => CreateNewTab(tab);
            return section;
        }

        void section_OnAfterExpanded(object sender, EventArgs e)
        {
            foreach (CategorySection<TCriteria, TCategory, TSearchAttribute> section in Categories)
                if (section != sender)
                    section.IsExpanded = false;
        }
    }
}
