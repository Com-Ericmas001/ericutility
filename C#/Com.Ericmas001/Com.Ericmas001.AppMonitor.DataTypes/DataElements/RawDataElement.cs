using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.Ericmas001.Util.Entities;

namespace Com.Ericmas001.AppMonitor.DataTypes.DataElements
{
    public class RawDataElement : BaseDataElement
    {

        private string m_Text;

        private string m_Html;

        public string Text
        {
            get { return m_Text; }
        }

        public string Html
        {
            get { return "<div style=\"white-space:nowrap; display:inline;\">" + m_Html + "</div>"; }
        }

        public RawDataElement(string text, string html)
        {
            m_Text = text;
            m_Html = html;
        }

        public RawDataElement(IDataItem item)
            : this(item.TextDescription, item.HtmlDescription)
        {
        }

        public override string Header
        {
            get { return "General"; }
        }
    }
}
