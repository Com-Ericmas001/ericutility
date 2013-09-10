using System;
using System.Collections.Generic;
using System.Text;

namespace EricUtilityNetworking.Downloader
{
    public interface ISegmentCalculator
    {
        CalculatedSegment[] GetSegments(int segmentCount, RemoteFileInfo fileSize);
    }
}
