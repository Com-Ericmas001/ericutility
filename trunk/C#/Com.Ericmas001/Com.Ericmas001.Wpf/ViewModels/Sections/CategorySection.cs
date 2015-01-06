using Com.Ericmas001.Wpf.Entities;

namespace Com.Ericmas001.Wpf.ViewModels.Sections
{
    public abstract class CategorySection<TCategory> : TabSection
        where TCategory : struct
    {

        public TCategory Category { get; private set; }

        public CategorySection(TCategory cat)
        {
            Category = cat;
            Info = new CategoryInfo<TCategory>(cat);
        }
    }
}
