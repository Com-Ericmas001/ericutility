using System;

namespace Com.Ericmas001.Util
{
    public static class TimeSpanFormatter
    {
        public static string ToString(TimeSpan ts)
        {
            if (ts == TimeSpan.MaxValue)
            {
                return "?";
            }

            string str = ts.ToString();
            int index = str.LastIndexOf('.');
            if (index > 0)
            {
                return str.Remove(index);
            }
            else
            {
                return str;
            }
        }
    }
}