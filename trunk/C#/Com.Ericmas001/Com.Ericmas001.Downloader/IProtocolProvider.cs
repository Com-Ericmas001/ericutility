using System.IO;

namespace Com.Ericmas001.Downloader
{
    public interface IProtocolProvider
    {
        // TODO: remove this method? Acoplamento ficara só de um lado
        void Initialize(Downloader downloader);

        Stream CreateStream(ResourceLocation rl, long initialPosition, long endPosition);

        RemoteFileInfo GetFileInfo(ResourceLocation rl, out Stream stream);
    }
}