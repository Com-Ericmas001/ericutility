namespace Com.Ericmas001.Net.Downloader
{
    public interface IMirrorSelector
    {
        void Init(Downloader downloader);

        ResourceLocation GetNextResourceLocation();
    }
}