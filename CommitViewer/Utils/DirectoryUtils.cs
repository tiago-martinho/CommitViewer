using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CommitViewer.Utils
{
    public static class DirectoryUtils
    {
        /// <summary>
        /// Deletes the specified directory and it's contents
        /// WARNING: USE IT WITH CARE
        /// </summary>
        /// <param name="directoryToDelete"></param>
        public static void DeleteDirectory(string directoryToDelete)
        {
            string[] files = Directory.GetFiles(directoryToDelete);
            string[] dirs = Directory.GetDirectories(directoryToDelete);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }

            Directory.Delete(directoryToDelete, false);
        }
    }
}
