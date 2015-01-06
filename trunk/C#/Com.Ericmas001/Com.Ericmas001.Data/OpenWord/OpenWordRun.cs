using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace Com.Ericmas001.Data.OpenWord
{
    public abstract class OpenWordRun
    {
        public abstract Run ObtainRun(WordprocessingDocument package);
    }
}
