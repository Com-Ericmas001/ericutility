using System;
using System.Collections.Generic;
using System.Text;

namespace EricUtilityNetworking.Downloader
{
    public enum SegmentState
    {
        Idle,
        Connecting,
        Downloading,
        Paused,
        Finished,
        Error,
    }
}
