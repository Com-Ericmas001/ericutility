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
            int ideb = text.IndexOf(before, startindex) + before.Length;
            if (ideb < before.Length)
                return null;
            int ifin = text.IndexOf(after, ideb);
            if (ifin < 0)
                return null;
            return text.Substring(ideb, ifin - ideb);
        }

        public static string RemoveHTMLTags(this string s)
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

        public static string RemoveBBCodeTags(this string s)
        {
            // Faster than regex: http://dotnetperls.com/remove-html-tags

            char[] array = new char[s.Length];
            int arrayIndex = 0;
            bool inside = false;

            for (int i = 0; i < s.Length; i++)
            {
                char let = s[i];
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
            byte[] data = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
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
    }
}