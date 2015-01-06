using DocumentFormat.OpenXml.Wordprocessing;

namespace Com.Ericmas001.Data.OpenWord.Runs
{

    public class OpenWordRunNewLine : OpenWordRunText
    {
        public OpenWordRunNewLine()
            : base(new Break())
        {

        }
    }
}
