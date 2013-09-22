using System.IO;

namespace EricUtility
{
    public static class PathHelper
    {
        public static string GetWithBackslash(string path)
        {
            if (!path.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                path += Path.DirectorySeparatorChar.ToString();
            }

            return path;
        }
    }
}