namespace Com.Ericmas001.Downloader
{
    public interface IMirrorSelector
    {
        void Init(Downloader downloader);

        ResourceLocation GetNextResourceLocation();
    }
}