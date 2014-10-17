using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
