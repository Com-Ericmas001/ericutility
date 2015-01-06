using System.Collections.Generic;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace Com.Ericmas001.Data.OpenWord.Runs
{
    public class OpenWordRunText : OpenWordRun
    {
        private readonly List<OpenXmlLeafElement> m_Parts = new List<OpenXmlLeafElement>();
        private readonly List<OpenXmlLeafElement> m_Properties = new List<OpenXmlLeafElement>();

        public List<OpenXmlLeafElement> Parts
        {
            get { return m_Parts; }
        }
        public List<OpenXmlLeafElement> Properties
        {
            get { return m_Properties; }
        }
        public OpenWordRunText(params OpenXmlLeafElement[] initialInfos)
        {
            if (initialInfos != null && initialInfos.Any())
                Parts.AddRange(initialInfos);
        }
        public OpenWordRunText(params string[] lines)
        {
            foreach (string l in lines)
            {
                Parts.Add(new Text(l));
                Parts.Add(new Break());
            }
            if (Parts.Any())
                Parts.Remove(Parts.Last());
        }

        public override Run ObtainRun(WordprocessingDocument package)
        {
            Run run = new Run();
            m_Parts.ForEach(x => run.AppendChild(x));
            run.RunProperties = new RunProperties();
            m_Properties.ForEach(x => run.RunProperties.Append(x));
            return run;
        }
    }
}
