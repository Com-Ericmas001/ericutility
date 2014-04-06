namespace Com.Ericmas001.Downloader
{
    public interface ISegmentCalculator
    {
        CalculatedSegment[] GetSegments(int segmentCount, RemoteFileInfo fileSize);
    }
}