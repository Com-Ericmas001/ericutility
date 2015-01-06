using DocumentFormat.OpenXml.Wordprocessing;

namespace Com.Ericmas001.Data.OpenWord.Runs
{
    public class OpenWordRunNewPage : OpenWordRunText
    {
        public OpenWordRunNewPage()
            : base(new Break { Type = BreakValues.Page })
        {

        }
    }
}
