using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Com.Ericmas001.Util
{
    public static class StringUtility
    {
        public static string Extract(this string text, string before, string after)
        {
            return Extract(text, before, after, 0);
        }

        public static string Extract(this string text, string before, string after, int startindex)
        {
            var ideb = text.IndexOf(before, startindex) + before.Length;
            if (ideb < before.Length)
                return null;
            var ifin = text.IndexOf(after, ideb);
            if (ifin < 0)
                return null;
            return text.Substring(ideb, ifin - ideb);
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

        public static string GetMd5Hash(this string input)
        {
            // Convert the input string to a byte array and compute the hash.
            var data = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            var sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data
            // and format each one as a hexadecimal string.
            for (var i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        public static bool IsValidEmail(this string strIn)
        {
            return Regex.IsMatch(strIn, @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*@((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$");
        }

        public static string CapitalizeFirstLetter(string s)
        {
            return s.First().ToString().ToUpper() + String.Join("", s.ToLower().Skip(1));
        }
        public static string RemoveExtraSpaces(string s)
        {
            return Regex.Replace(s, @"\s+", " ").Trim();
        }
    }
}