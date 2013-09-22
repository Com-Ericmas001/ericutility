namespace EricUtilityNetworking.Downloader
{
    public interface IMirrorSelector
    {
        void Init(Downloader downloader);

        ResourceLocation GetNextResourceLocation();
    }
}