﻿using System;
using System.Collections.Generic;
using System.Linq;
using Com.Ericmas001.AppMonitor.DataTypes.Attributes;
using Com.Ericmas001.AppMonitor.DataTypes.Entities;
using Com.Ericmas001.AppMonitor.DataTypes.Helpers;
using Com.Ericmas001.Wpf.Entities.Enums;
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

        private readonly CategoryInfo<TCategory> m_Info;

        private bool m_IsExpanded;
        private readonly TCriteria[] m_Criterias;
        private readonly Dictionary<TCriteria, BaseSearchElement> m_CriteriaModels = new Dictionary<TCriteria, BaseSearchElement>();
        private TCriteria m_SelectedCriteria;

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
            get { return m_SelectedCriteria.Equals(default(TCriteria)) ? null : m_CriteriaModels[m_SelectedCriteria]; }
        }

        public string SelectedCriteriaValue
        {
            get { return SelectedCriteriaModel == null ? null : SelectedCriteriaModel.TextValue; }
        }

        public TCriteria[] Criterias
        {
            get { return m_Criterias; }
        }

        public CategoryInfo<TCategory> Info
        {
            get { return m_Info; }
        }

        public enum CriteriaNumberEnum
        {
            Zero,
            One,
            Plenty
        }

        public CriteriaNumberEnum NumberOfCriterias
        {
            get { return m_Criterias.Length == 0 ? CriteriaNumberEnum.Zero : m_Criterias.Length == 1 ? CriteriaNumberEnum.One : CriteriaNumberEnum.Plenty; }
        }

        public CategorySection(TCategory cat)
        {
            Category = cat;
            m_Info = new CategoryInfo<TCategory>(cat);

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
            SelectedCriteria = NumberOfCriterias == CriteriaNumberEnum.Zero ? default(TCriteria) : ObtainDefaultCriteria();
        }

        protected virtual TCriteria ObtainDefaultCriteria()
        {
            return Criterias.First();
        }

        protected abstract IEnumerable<string> ObtainList(TCriteria crit);
    }
}
