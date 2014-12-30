using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using Com.Ericmas001.AppMonitor.DataTypes.Attributes;
using Com.Ericmas001.AppMonitor.DataTypes.Helpers;
using Com.Ericmas001.Util;
using Com.Ericmas001.Util.Attributes;
using Com.Ericmas001.Wpf.Entities.Enums;
using Com.Ericmas001.Wpf.Helpers;
using Com.Ericmas001.Wpf.ViewModels.SearchElements;
using Com.Ericmas001.Wpf.ViewModels.Tabs;

namespace Com.Ericmas001.AppMonitor.DataTypes.ViewModels.Sections
{
    public abstract class CategorySection<TCriteria, TCategory, TSearchAttribute> : NewTabViewModel
        where TCriteria : struct
        where TCategory : struct
        where TSearchAttribute : Attribute, ISearchCriteriaAttribute<TCategory>
    {
        public event EventHandler OnAfterExpanded = delegate { };

        private SolidColorBrush m_Background = Brushes.Gray;
        private Color m_ButtonColor = Colors.DarkSlateGray;
        public virtual string TabHeader { get { return null; } }

        private string m_IconSmallImageName = String.Empty;
        private string m_IconBigImageName = String.Empty;

        public virtual ImageSource IconImageSmall
        {
            get { return String.IsNullOrEmpty(m_IconSmallImageName) ? null : Application.Current.FindResource(m_IconSmallImageName) as ImageSource; }
        }
        public virtual ImageSource IconImageBig
        {
            get { return String.IsNullOrEmpty(m_IconBigImageName) ? null : Application.Current.FindResource(m_IconBigImageName) as ImageSource; }
        }

        private bool m_IsExpanded;
        private TCriteria[] m_Criterias;
        private Dictionary<TCriteria, BaseSearchElement> m_CriteriaModels = new Dictionary<TCriteria, BaseSearchElement>();
        private TCriteria m_SelectedCriteria;

        public SolidColorBrush Background
        {
            get { return m_Background; }
        }
        public Color ButtonBrush
        {
            get { return m_ButtonColor; }
        }

        public SolidColorBrush HeaderForeground
        {
            get { return ColorHelper.GetForegroundFromBackground(Background); }
        }

        public bool IsExpanded
        {
            get { return m_IsExpanded; }
            set
            {
                if (m_IsExpanded != value)
                {
                    m_IsExpanded = value;
                    RaisePropertyChanged("IsExpanded");
                    if (m_IsExpanded)
                        OnAfterExpanded(this, new EventArgs());
                }
            }
        }

        public TCategory Category { get; private set; }

        public Dictionary<TCriteria, BaseSearchElement> CriteriaModels
        {
            get { return m_CriteriaModels; }
        }

        public TCriteria SelectedCriteria
        {
            get { return m_SelectedCriteria; }
            set
            {
                m_SelectedCriteria = value;
                RaisePropertyChanged("SelectedCriteria");
                RaisePropertyChanged("SelectedCriteriaModel");
            }
        }

        public BaseSearchElement SelectedCriteriaModel
        {
            get { return m_CriteriaModels[m_SelectedCriteria]; }
        }

        public TCriteria[] Criterias
        {
            get { return m_Criterias; }
        }

        public CategorySection(TCategory cat)
        {
            Category = cat;

            var imgAtt = EnumFactory<TCategory>.GetAttribute<ImageSourceAttribute>(cat);
            if (imgAtt != null)
            {
                m_IconSmallImageName = imgAtt.ImageNameSmall;
                m_IconBigImageName = imgAtt.ImageNameBig;
            }

            var brushAtt = EnumFactory<TCategory>.GetAttribute<ColorAttribute>(cat);
            if (brushAtt != null)
            {
                try
                {
                    var bc = new BrushConverter();
                    m_Background = (SolidColorBrush)bc.ConvertFromString(brushAtt.Color);
                }
                catch { }
            }

            var bbrushAtt = EnumFactory<TCategory>.GetAttribute<ButtonColorAttribute>(cat);
            if (bbrushAtt != null)
            {
                try
                {
                    var bc = new BrushConverter();
                    m_ButtonColor = ((SolidColorBrush)bc.ConvertFromString(bbrushAtt.Color)).Color;
                }
                catch { }
            }

            var criterias = CriteriaHelper<TCriteria, TCategory>.GetAllSearchCriterias<TSearchAttribute>(cat);
            m_Criterias = criterias.Keys.ToArray();

            var myTextSearchElement = new TextSearchElement();
            myTextSearchElement.ValueSubmitted += delegate { StartNewTabCommand.Execute(null); };

            var myUpperTextSearchElement = new UpperTextSearchElement();
            myTextSearchElement.ValueSubmitted += delegate { StartNewTabCommand.Execute(null); };

            var myIntSearchElement = new IntSearchElement();
            myTextSearchElement.ValueSubmitted += delegate { StartNewTabCommand.Execute(null); };

            foreach (var crit in criterias.Keys)
                switch (criterias[crit])
                {
                    case SearchTypeEnum.Date:
                        m_CriteriaModels.Add(crit, new DateSearchElement());
                        break;
                    case SearchTypeEnum.Guid:
                        m_CriteriaModels.Add(crit, new GuidSearchElement());
                        break;
                    case SearchTypeEnum.Int:
                        m_CriteriaModels.Add(crit, myIntSearchElement);
                        break;
                    case SearchTypeEnum.List:
                        var myListSearchElement = new ListSearchElement(ObtainList(crit));
                        myListSearchElement.ValueSubmitted += delegate { StartNewTabCommand.Execute(null); };
                        m_CriteriaModels.Add(crit, myListSearchElement);
                        break;
                    case SearchTypeEnum.UpperText:
                        m_CriteriaModels.Add(crit, myUpperTextSearchElement);
                        break;
                    default:
                        m_CriteriaModels.Add(crit, myTextSearchElement);
                        break;
                }
            SelectedCriteria = Criterias.First();
        }

        protected abstract IEnumerable<string> ObtainList(TCriteria crit);
    }
}
