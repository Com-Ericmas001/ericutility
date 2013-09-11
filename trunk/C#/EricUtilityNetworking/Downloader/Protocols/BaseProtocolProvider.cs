using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace EricUtilityNetworking.Downloader.Protocols
{
    public class BaseProtocolProvider
    {
        static BaseProtocolProvider()
        {
            ServicePointManager.DefaultConnectionLimit = int.MaxValue;
        }

        protected WebRequest GetRequest(ResourceLocation location)
        {
            WebRequest request = WebRequest.Create(location.URL);
            request.Timeout = 30000;
            return request;
        }
    }
}
