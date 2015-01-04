using System;
using System.Collections.Generic;
using System.Linq;
using Com.Ericmas001.AppMonitor.DataTypes.ViewModels.Sections;
using Com.Ericmas001.Util;
using Com.Ericmas001.Util.Attributes;
using Com.Ericmas001.Wpf.ViewModels.Tabs;

namespace Com.Ericmas001.AppMonitor.DataTypes.ViewModels
{
    public abstract class NewSearchViewModel<TCategory> : NewTabViewModel
        where TCategory : struct
    {

        public override BaseTabViewModel CreateContentTab()
        {
            return null;
        }

        private readonly List<TabSection> m_Sections = new List<TabSection>();

        public TabSection[] Sections
        {
            get { return m_Sections.ToArray(); }
        }

        protected virtual IEnumerable<TCategory> ExcludeCategories(IEnumerable<TCategory> categories)
        {
            return categories;
        }

        private void AddAllCategoriesSection()
        {
            m_Sections.AddRange(ExcludeCategories(EnumFactory<TCategory>.AllValues).OrderBy(x => EnumFactory<TCategory>.GetAttribute<PriorityAttribute>(x).Priority).ThenBy(EnumFactory<TCategory>.ToString).Select(x => CreateSectionWithHandlers(CreateCategorySection(x))));
        }

        protected virtual void AddAllSections()
        {
            AddAllCategoriesSection();
        }

        protected void AddSection(TabSection section)
        {
            m_Sections.Add(CreateSectionWithHandlers(section));
        }

        protected virtual TabSection CreateCategorySection(TCategory cat)
        {
            throw new NotImplementedException("You must override CreateCategorySection to use it !");
        }

        public NewSearchViewModel()
        {
            m_Sections = new List<TabSection>();

            AddAllSections();

            if (Sections.Any())
                Sections.First().IsExpanded = true;

        }

        private TabSection CreateSectionWithHandlers(TabSection section)
        {
            section.OnAfterExpanded += section_OnAfterExpanded;
            section.OnTabCreation += (sender, tab) => CreateNewTab(tab);
            return section;
        }

        void section_OnAfterExpanded(object sender, EventArgs e)
        {
            foreach (TabSection section in Sections)
                if (section != sender)
                    section.IsExpanded = false;
        }
    }
}
