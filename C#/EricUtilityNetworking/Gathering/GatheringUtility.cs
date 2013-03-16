using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Drawing;

namespace EricUtility.Networking.Gathering
{
    public class GatheringUtility
    {

        public static CookieContainer SignInWebsite(string loginUrl, string args, bool usePOST)
        {
            HttpWebRequest request;
            if (usePOST)
            {
                request = (HttpWebRequest)WebRequest.Create(loginUrl);
                request.Method = "POST";
                UTF8Encoding encoding = new UTF8Encoding();
                byte[] bytes = encoding.GetBytes(args);
                request.UserAgent = "Opera/9.80 (Windows NT 6.1; U; Edition Next; en) Presto/2.10.229 Version/12.00";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = bytes.Length;
                using (Stream writeStream = request.GetRequestStream())
                {
                    writeStream.Write(bytes, 0, bytes.Length);
                }
            }
            else
            {
                request = (HttpWebRequest)WebRequest.Create(loginUrl + "?" + args);
                request.Method = "GET";

                request.AllowAutoRedirect = true;
                request.UserAgent = "Opera/9.80 (Windows NT 6.1; U; Edition Next; en) Presto/2.10.229 Version/12.00";
                request.CookieContainer = new CookieContainer();
            }
            request.CookieContainer = new CookieContainer();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            CookieContainer container = request.CookieContainer;
            response.Close();
            return container;
        }
        private static HttpWebResponse GetResponse(string url, CookieContainer cookies)
        {
            HttpWebResponse res = null;
            for (int i = 0; i < 10; ++i)
            {
                try
                {
                    HttpWebRequest request;
                    request = (HttpWebRequest)WebRequest.Create(url);
                    request.AllowAutoRedirect = true;
                    request.UserAgent = "Opera/9.80 (Windows NT 6.1; U; Edition Next; en) Presto/2.10.229 Version/12.00";
                    request.CookieContainer = cookies;
                    request.Timeout = 10000;
                    res = (HttpWebResponse)request.GetResponse();
                    return res;
                }
                catch { }
            }
            return res;
        }
        public static String GetPageSource(string url, string postArgs)
        {
            return GetPageSource(url, new CookieContainer(), postArgs);
        }
        public static String GetPageSource(string url, string postArgs, string contentType)
        {
            return GetPageSource(url, new CookieContainer(), postArgs, contentType);
        }
        public static String GetPageSource(string url, CookieContainer cookies, string postArgs, string contentType)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] bytes = encoding.GetBytes(postArgs);
            request.ContentType = contentType;
            request.UserAgent = "Opera/9.80 (Windows NT 6.1; U; Edition Next; en) Presto/2.10.229 Version/12.00";
            request.ContentLength = bytes.Length;
            using (Stream writeStream = request.GetRequestStream())
            {
                writeStream.Write(bytes, 0, bytes.Length);
            }
            request.CookieContainer = cookies;
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream s = response.GetResponseStream();
                string charSet = response.CharacterSet;
                Encoding coding;
                if (String.IsNullOrEmpty(charSet))
                    coding = Encoding.Default;
                else
                    coding = Encoding.GetEncoding(charSet);
                string res = new StreamReader(s,coding).ReadToEnd();
                s.Close();
                response.Close();
                return res;
            }
            catch
            {
                return "";
            }
        }
        public static String GetPageUrl(string url, CookieContainer cookies)
        {
            return GetPageUrl(url, cookies, "", "application/x-www-form-urlencoded ; charset=UTF-8");
        }
        public static String GetPageUrl(string url, CookieContainer cookies, string postArgs, string contentType)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] bytes = encoding.GetBytes(postArgs);
            request.ContentType = contentType;
            request.UserAgent = "Opera/9.80 (Windows NT 5.1; U; Edition Campaign 21; fr) Presto/2.6.30 Version/10.63";
            request.ContentLength = bytes.Length;
            using (Stream writeStream = request.GetRequestStream())
            {
                writeStream.Write(bytes, 0, bytes.Length);
            }
            request.CookieContainer = cookies;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream s = response.GetResponseStream();
            string res = response.ResponseUri.ToString();
            s.Close();
            response.Close();
            return res;
        }
        public static String GetPageSource(string url, CookieContainer cookies, string postArgs)
        {
            return GetPageSource(url, cookies, postArgs, "application/x-www-form-urlencoded ; charset=UTF-8");
        }
        public static String GetPageSource(string url, CookieContainer cookies)
        {
            HttpWebResponse response = GetResponse(url,cookies);
            if (response != null)
            {
                Stream s = response.GetResponseStream();
                string charSet = response.CharacterSet;
                Encoding coding;
                if (String.IsNullOrEmpty(charSet))
                    coding = Encoding.Default;
                else
                    coding = Encoding.GetEncoding(charSet);
                string res = new StreamReader(s, coding).ReadToEnd();
                s.Close();
                response.Close();
                return res;
            }
            else
                return null;
        }

        public static String GetPageSource(string url)
        {
            return GetPageSource(url,new CookieContainer());
        }
        public static Image GetImage(string url, CookieContainer cookies)
        {
            HttpWebResponse response = GetResponse(url, cookies);
            Stream s = response.GetResponseStream();
            Image i = Image.FromStream(s);
            return i;
        }

        public static Image GetImage(string url)
        {
            return GetImage(url, new CookieContainer());
        }
    }
}
