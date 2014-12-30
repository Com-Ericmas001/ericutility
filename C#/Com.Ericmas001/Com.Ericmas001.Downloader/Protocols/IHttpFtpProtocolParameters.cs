namespace Com.Ericmas001.Downloader.Protocols
{
    public interface IHttpFtpProtocolParameters
    {
        string ProxyAddress { get; set; }

        string ProxyUserName { get; set; }

        string ProxyPassword { get; set; }

        string ProxyDomain { get; set; }

        bool UseProxy { get; set; }

        bool ProxyByPassOnLocal { get; set; }

        int ProxyPort { get; set; }
    }
}