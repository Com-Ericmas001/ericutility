using EricUtilityNetworking.Downloader;
using System.Windows.Forms;

namespace EricUtility.Windows.Forms.Downloader
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