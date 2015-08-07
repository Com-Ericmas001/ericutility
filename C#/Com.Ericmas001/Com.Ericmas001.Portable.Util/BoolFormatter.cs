namespace Com.Ericmas001.Portable.Util
{
    public static class BoolFormatter
    {
        private const string YES = "Yes";
        private const string NO = "No";

        public static bool FromString(string s)
        {
            if (s == YES) return true;
            return false;
        }

        public static string ToString(bool v)
        {
            if (v) return YES;
            return NO;
        }
    }
}