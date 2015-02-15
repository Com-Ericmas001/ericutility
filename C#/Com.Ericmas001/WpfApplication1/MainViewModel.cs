using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Ericmas001.Util;
using Com.Ericmas001.Util.Entities;
using Com.Ericmas001.Util.Entities.Filters.Enums;
using Com.Ericmas001.Wpf;
using Com.Ericmas001.Wpf.Entities.Filters;
using Com.Ericmas001.Wpf.ViewModels.Sections;

namespace WpfApplication1
{
    public enum TestEnum
    {
        [Description("Non mais Allo quoi !")]
        Allo,
        [Description("Bien le Bonjour !")]
        Bonjour,
        [Description("Aurevoir !")]
        Bye
    }

    public class SimpleDataItem : IDataItem
    {

        public string ObtainValue(string field)
        {
            return "field_aaa";
        }

        public string ObtainFilterValue(string field)
        {
            return ObtainValue(field);
        }

        public string TextDescription
        {
            get { return "TextDescription"; }
        }

        public string HtmlDescription
        {
            get { return "HtmlDescription"; }
        }
    }
    public class MainViewModel
    {
        public ChooseGroupViewModel ChooseGroupVm { get; private set; }

        private TestEnum m_TestEnumValue = TestEnum.Bonjour;
        public TestEnum TestEnumValue { get { return m_TestEnumValue; } }

        public MainViewModel()
        {
            string blabla = "Je vais a l'étable pour tirer ma vache bleue !";
            string between = blabla.InfoBetween("pour ", " ma");
            string betweennot = blabla.InfoBetween("pour ", " mon");
            string beforeJe = blabla.InfoBefore("Je", 5);
            string beforeVais = blabla.InfoBefore("vais", 5);
            string beforeVache = blabla.InfoBefore("vache", 5);
            string afterJe = blabla.InfoAfter("Je", 5);
            string aftervache = blabla.InfoAfter("vache", 15);
            string afterExcl = blabla.InfoAfter("!", 5);
            ChooseGroupVm = new ChooseGroupViewModel(EnumFactory<TestEnum>.AllValues.Select(x => EnumFactory<TestEnum>.ToString(x)), x => x);
            ChooseGroupVm.AvailablesGroups.Items.ToList().ForEach(x => ChooseGroupVm.AddFieldToFilter(x, new BaseFilterInCreation[] { new SimpleFilterInCreation(x, FilterEnum.Text, new BunchOfDataItems<SimpleDataItem>() { Data = new SimpleDataItem[] { new SimpleDataItem() } }) }));
        }
    }
}
