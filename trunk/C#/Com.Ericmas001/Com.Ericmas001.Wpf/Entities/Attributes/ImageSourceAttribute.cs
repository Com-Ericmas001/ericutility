using System;

namespace Com.Ericmas001.Wpf.Entities.Attributes
{
    public class ImageSourceAttribute : Attribute
    {
        public string ImageNameSmall { get; private set; }
        public string ImageNameBig { get; private set; }

        public ImageSourceAttribute(string imageNameSmall, string imageNameBig)
        {
            ImageNameSmall = imageNameSmall;
            ImageNameBig = imageNameBig;
        }
    }
}
