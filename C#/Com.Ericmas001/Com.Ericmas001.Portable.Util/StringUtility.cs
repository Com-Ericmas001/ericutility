using System;
using System.Text.RegularExpressions;

namespace Com.Ericmas001.Portable.Util
{
    public static class StringUtility
    {
        public static string Extract(this string text, string before, string after, int startindex = 0)
        {
            int ideb = text.IndexOf(before, startindex) + before.Length;
            if (ideb < before.Length)
                return null;
            int ifin = text.IndexOf(after, ideb);
            if (ifin < 0)
                return null;
            return text.Substring(ideb, ifin - ideb);
        }

        public static string InfoBetween(this string text, string before, string after, int startindex = 0)
        {
            return Extract(text, before, after, startindex);

        }
        public static string AllInfoBefore(this string text, string keyword, int startindex = 0)
        {
            int ifin = text.IndexOf(keyword, startindex);
            if (ifin < startindex)
                return null;
            return text.Remove(ifin);
        }
        public static string InfoBefore(this string text, string keyword, int length, int startindex = 0)
        {
            int ifin = text.IndexOf(keyword, startindex);
            if (ifin < startindex)
                return null;
            int ideb = Math.Max(startindex, ifin - length);
            return text.Substring(ideb, Math.Min(length, ifin - ideb));
        }

        public static string AllInfoAfter(this string text, string keyword, int startindex = 0)
        {
            int ideb = text.IndexOf(keyword, startindex) + keyword.Length;
            if (ideb < keyword.Length)
                return null;
            return text.Substring(ideb);
        }


        public static string InfoAfter(this string text, string keyword, int length, int startindex = 0)
        {
            int ideb = text.IndexOf(keyword, startindex) + keyword.Length;
            if (ideb < keyword.Length)
                return null;
            return text.Substring(ideb, Math.Min(length, text.Length - ideb));
        }

        public static string RemoveHtmlTags(this string s)
        {
            // Faster than regex: http://dotnetperls.com/remove-html-tags

            var array = new char[s.Length];
            var arrayIndex = 0;
            var inside = false;

            for (var i = 0; i < s.Length; i++)
            {
                var let = s[i];
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

        public static string RemoveBbCodeTags(this string s)
        {
            // Faster than regex: http://dotnetperls.com/remove-html-tags

            var array = new char[s.Length];
            var arrayIndex = 0;
            var inside = false;

            for (var i = 0; i < s.Length; i++)
            {
                var let = s[i];
                if (let == '[')
                {
                    inside = true;
                    continue;
                }
                if (let == ']')
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

        public static bool IsValidEmail(this string strIn)
        {
            return Regex.IsMatch(strIn, @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*@((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$");
        }
        public static string CapitalizeFirstLetter(this string s)
        {
            return char.ToUpper(s[0]) + s.Substring(1);
        }
        public static string RemoveExtraSpaces(this string s)
        {
            return Regex.Replace(s, @"\s+", " ").Trim();
        }
    }
}