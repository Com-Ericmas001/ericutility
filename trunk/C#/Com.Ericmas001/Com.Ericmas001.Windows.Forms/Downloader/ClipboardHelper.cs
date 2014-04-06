using Com.Ericmas001.Net.Downloader;
using System.Windows.Forms;

namespace Com.Ericmas001.Windows.Forms.Downloader
{
    public static class ClipboardHelper
    {
        public static string GetURLOnClipboard()
        {
            string url = string.Empty;

            if (Clipboard.ContainsText())
            {
                string tempUrl = Clipboard.GetText();

                if (ResourceLocation.IsURL(tempUrl))
                {
                    url = tempUrl;
                }
                else
                {
                    tempUrl = null;
                }
            }

            return url;
        }
    }
}