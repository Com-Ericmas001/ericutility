using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Ericmas001.Util;

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
    public class MainViewModel
    {
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
        }
    }
}
