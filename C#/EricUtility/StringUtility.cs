using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace EricUtility
{
    public class StringUtility
    {
        private static string HtmlEncode(string text)
        {
            char[] chars = HttpUtility.HtmlEncode(text).ToCharArray();
            StringBuilder result = new StringBuilder(text.Length + (int)(text.Length * 0.1));

            foreach (char c in chars)
            {
                int value = Convert.ToInt32(c);
                if (value > 127 || value == 32)
                    result.AppendFormat("&#{0};", value);
                else
                    result.Append(c);
            }

            return result.ToString();
        }
        public static string EncodeString(string text)
        {
            return HtmlEncode(text);
        }
        public static string EncodeUrl(string text)
        {
            return HttpUtility.UrlEncode(text);
        }
        public static string DecodeString(string text)
        {
            return HttpUtility.HtmlDecode(text);
        }
        public static string DecodeURL(string text)
        {
            return HttpUtility.UrlDecode(text);
        }
        public static string Extract(string text, string before, string after)
        {
            return Extract(text, before, after, 0);
        }
        public static string Extract(string text, string before, string after, int startindex)
        {
            int ideb = text.IndexOf(before, startindex) + before.Length;
            if (ideb < before.Length)
                return null;
            int ifin = text.IndexOf(after, ideb);
            if (ifin < 0)
                return null;
            return text.Substring(ideb, ifin - ideb);
        }
        public static string RemoveHTMLTags(string s)
        {
            // Faster than regex: http://dotnetperls.com/remove-html-tags

            char[] array = new char[s.Length];
            int arrayIndex = 0;
            bool inside = false;

            for (int i = 0; i < s.Length; i++)
            {
                char let = s[i];
                if (let == '<')
                {
                    inside = true;
                    continue;
                }
                if (let == '>')
                {
                    inside = false;
                    continue;
                }
                if (!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }
            return new string(array, 0, arrayIndex);
        }
    }
}
