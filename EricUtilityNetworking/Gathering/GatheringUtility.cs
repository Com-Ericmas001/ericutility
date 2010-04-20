using System;
using System.Collections.Generic;
using System.Linq;
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
            HttpWebRequest request;
            request = (HttpWebRequest)WebRequest.Create(url);
            request.AllowAutoRedirect = true;
            request.CookieContainer = cookies;
            return (HttpWebResponse)request.GetResponse();
        }
        public static String GetPageSource(string url, CookieContainer cookies)
        {
            HttpWebResponse response = GetResponse(url,cookies);
            Stream s = response.GetResponseStream();
            string res = new StreamReader(s).ReadToEnd();
            s.Close();
            response.Close();
            return res;
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
