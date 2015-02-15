using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Com.Ericmas001.Util;
using Com.Ericmas001.Util.Entities;
using Com.Ericmas001.Util.Entities.Filters.Enums;
using Com.Ericmas001.Wpf;
using Com.Ericmas001.Wpf.Entities.Filters;
using Com.Ericmas001.Wpf.ViewModels.Sections;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

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
        private RelayCommand m_SaveToJsonCommand;
        private RelayCommand m_LoadFromJsonCommand;

        public ICommand SaveToJsonCommand
        {
            get { return m_SaveToJsonCommand ?? (m_SaveToJsonCommand = new RelayCommand(x => SaveToJson())); }
        }
        public ICommand LoadFromJsonCommand
        {
            get { return m_LoadFromJsonCommand ?? (m_LoadFromJsonCommand = new RelayCommand(x => LoadFromJson())); }
        }

        private void SaveToJson()
        {
            File.WriteAllText("test.txt", JsonConvert.SerializeObject(ChooseGroupVm.CurrentFilters.ToArray(), Formatting.Indented, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All, ContractResolver = new WritablePropertiesOnlyResolver() }));
        }
        private void LoadFromJson()
        {
            string str = File.ReadAllText("test.txt");
            BaseCompiledFilter[] filters = JsonConvert.DeserializeObject<BaseCompiledFilter[]>(str, new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.All});
            foreach(BaseCompiledFilter bcf in filters)
                ChooseGroupVm.AddCompiledFilter(bcf);
        }
        class WritablePropertiesOnlyResolver : DefaultContractResolver
        {
            protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
            {
                IList<JsonProperty> props = base.CreateProperties(type, memberSerialization);
                return props.Where(p => p.Writable).ToList();
            }
        }
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
