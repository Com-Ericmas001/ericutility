using System;

namespace Com.Ericmas001.Util
{
    public static class ByteFormatter
    {
        private const long KB = 1024;
        private const long MB = KB * 1024;
        private const long GB = MB * 1024;

        private const string B_FORMAT_PATTERN = "{0} b";
        private const string KB_FORMAT_PATTERN = "{0:0} KB";
        private const string MB_FORMAT_PATTERN = "{0:0,###} MB";
        private const string GB_FORMAT_PATTERN = "{0:0,###.###} GB";

        public static string ToString(long size)
        {
            if (size < KB)
            {
                return String.Format(B_FORMAT_PATTERN, size);
            }
            if (size >= KB && size < MB)
            {
                return String.Format(KB_FORMAT_PATTERN, size / 1024.0f);
            }
            if (size >= MB && size < GB)
            {
                return String.Format(MB_FORMAT_PATTERN, size / 1024.0f);
            }
            return String.Format(GB_FORMAT_PATTERN, size / 1024.0f);
        }
    }
}