using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Com.Ericmas001.Data.OpenWord.Runs;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace Com.Ericmas001.Data.OpenWord
{
    public class OpenWordParagraph
    {
        private readonly List<OpenWordRun> m_Runs = new List<OpenWordRun>();
        private JustificationValues m_Align = JustificationValues.Left;
        private string m_Style = String.Empty;

        public JustificationValues Align
        {
            get { return m_Align; }
            set { m_Align = value; }
        }

        public string Style
        {
            get { return m_Style; }
            set { m_Style = value; }
        }

        public List<OpenWordRun> Runs
        {
            get { return m_Runs; }
        }

        public virtual Paragraph ObtainParagraph(WordprocessingDocument package)
        {
            var par = new Paragraph();
            par.ParagraphProperties = new ParagraphProperties();
            par.ParagraphProperties.Justification = new Justification { Val = Align };
            Runs.ForEach(x => par.AppendChild(x.ObtainRun(package)));
            return par;
        }

        public void AddLines(params string[] lines)
        {
            Runs.Add(new OpenWordRunText(lines));
        }
        public void AddImage(byte[] image, Size size)
        {
            Runs.Add(new OpenWordRunImage(image, size));
        }
        public void AddRuns(params OpenWordRun[] runs)
        {
            runs.ToList().ForEach(x => Runs.Add(x));
        }
        public void AddLineBreak()
        {
            AddRuns(new OpenWordRunNewLine());
        }
        public void AddPageBreak()
        {
            AddRuns(new OpenWordRunNewLine());
        }

    }
}
