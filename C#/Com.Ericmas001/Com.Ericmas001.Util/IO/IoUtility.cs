using Com.Ericmas001.Portable.Util;
using System;
using System.IO;

namespace Com.Ericmas001.Util.IO
{
    public class IoUtility
    {

        public static void CopyFolderAndContent(string srcFolderPath, string destFolderPath)
        {
            var srcInfo = new DirectoryInfo(srcFolderPath);
            var srcDirs = srcInfo.GetDirectories();
            foreach (var dir in srcDirs)
            {
                Directory.CreateDirectory(Path.Combine(destFolderPath, dir.Name));
                CopyFolderAndContent(Path.Combine(srcFolderPath, dir.Name), Path.Combine(destFolderPath, dir.Name));
            }
            var srcFiles = srcInfo.GetFiles();
            foreach (var file in srcFiles)
            {
                try
                {
                    file.CopyTo(Path.Combine(destFolderPath, file.Name), true);
                }
                catch
                {
                    Portable.Util.LogManager.Log(LogLevel.Error, "IoUtility.CopyFolderAndContent", String.Format("Impossible de copier {0}.", file.Name));
                }
            }
        }

        public static string CreateTemporaryFolder(string rootPath, string filename)
        {
            var tempFolderName = filename.Split('.')[0];
            var tempFolderRoot = Path.Combine(rootPath, tempFolderName);
            if (Directory.Exists(tempFolderRoot))
                Directory.Delete(tempFolderRoot, true);
            Directory.CreateDirectory(tempFolderRoot);
            return tempFolderRoot;
        }

        public static string CreateHierarchicFolder(string rootPath, string path)
        {
            var tempFolderPath = rootPath;
            var hierarchie = path.Split('/');
            for (var i = 0; i < hierarchie.Length; ++i)
            {
                tempFolderPath = Path.Combine(tempFolderPath, hierarchie[i]);
                if (!Directory.Exists(tempFolderPath))
                    Directory.CreateDirectory(tempFolderPath);
            }
            return tempFolderPath;
        }

        public static void RemoveHierarchicFolder(string rootPath, string path)
        {
            var fullPath = Path.Combine(rootPath, path);
            var canDelete = true;
            while (canDelete && fullPath.Length > rootPath.Length)
            {
                var di = new DirectoryInfo(fullPath);
                if (di.GetDirectories().Length > 0 || di.GetFiles().Length > 0)
                    canDelete = false;
                else
                {
                    Directory.Delete(fullPath);
                    if (di.Parent != null) fullPath = di.Parent.FullName;
                }
            }
        }
    }
}