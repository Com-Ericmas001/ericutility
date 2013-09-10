using System;
using System.Collections.Generic;
using System.Text;

namespace EricUtilityNetworking.Downloader
{
    public interface IMirrorSelector
    {
        void Init(Downloader downloader);

        ResourceLocation GetNextResourceLocation(); 
    }
}
