using System;

namespace Com.Ericmas001.Portable.Util
{
    public static class TimeSpanFormatter
    {
        public static string ToString(TimeSpan ts)
        {
            if (ts == TimeSpan.MaxValue)
            {
                return "?";
            }

            var str = ts.ToString();
            var index = str.LastIndexOf('.');
            if (index > 0)
            {
                return str.Remove(index);
            }
            return str;
        }
    }
}