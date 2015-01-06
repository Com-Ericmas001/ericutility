using System.Collections.Generic;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace Com.Ericmas001.Data.OpenWord
{
    public class OpenWordDocument
    {
        private List<OpenWordParagraph> m_Paragraphes = new List<OpenWordParagraph>();
        private string m_FileName;
        public OpenWordDocument(string filename)
        {
            m_FileName = filename;
        }
        public void WriteDocument()
        {
            using (WordprocessingDocument package = WordprocessingDocument.Create(m_FileName, WordprocessingDocumentType.Document))
            {
                // Add a new main document part. 
                package.AddMainDocumentPart();

                // Create the Document DOM. 
                Document doc = new Document(new Body());
                package.MainDocumentPart.Document = doc;

                m_Paragraphes.ForEach(x => doc.Body.AppendChild(x.ObtainParagraph(package)));
                
                // Save changes to the main document part. 
                package.MainDocumentPart.Document.Save();
            }
        }
        public OpenWordParagraph NewParagraph(JustificationValues align = JustificationValues.Left)
        {
            var par = new OpenWordParagraph();
            par.Align = align;
            m_Paragraphes.Add(par);
            return par;
        }

        public void NewPage()
        {
            NewParagraph().AddPageBreak();
        }
    }
}
