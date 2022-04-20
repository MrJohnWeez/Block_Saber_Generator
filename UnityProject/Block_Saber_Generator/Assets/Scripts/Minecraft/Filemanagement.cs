using System;
using System.Collections.Generic;
using System.IO;
using Utilities.Wrappers;

namespace Minecraft
{
    /// <summary>
    /// Generic file management that is common for Minecraft datapack and resource
    /// pack generation
    /// </summary>
    public static class Filemanagement
    {
        /// <summary>
        /// Update all files within a directory to correct variable names
        /// </summary>
        /// <param name="folderPath">In folder path</param>
        /// <param name="keyVars">dictionary of words to replace within files</param>
        /// <param name="checkSubDirectories">Recursive update files and folders</param>
        /// <param name="excludeExtensions">Blacklist of file extensions not to update</param>
        public static void UpdateAllCopiedFiles(string folderPath, Dictionary<string, string> keyVars, bool checkSubDirectories = false, string[] excludeExtensions = null)
        {
            if (checkSubDirectories)
            {
                string[] dirs = SafeFileManagement.GetDirectoryPaths(folderPath, Globals.NUMBER_OF_IO_RETRY_ATTEMPTS);
                foreach (string dir in dirs)
                {
                    UpdateAllCopiedFiles(dir, keyVars, checkSubDirectories, excludeExtensions);
                }
            }

            if (Directory.Exists(folderPath))
            {
                string[] files = SafeFileManagement.GetFilesPaths(folderPath, Globals.NUMBER_OF_IO_RETRY_ATTEMPTS);
                foreach (string file in files)
                {
                    if (excludeExtensions != null)
                    {
                        string extension = Path.GetExtension(file);
                        if (Array.Exists(excludeExtensions, element => element.ToLower() == extension.ToLower()))
                        {
                            continue;
                        }
                    }
                    UpdateFileWithKeys(file, keyVars);
                }
            }
        }

        /// <summary>
        /// Replace any keys within a file from a dictionary
        /// </summary>
        /// <param name="filePath">path of file to replace keys in</param>
        /// <param name="keyVars">dictionary of words to replace within files</param>
        public static void UpdateFileWithKeys(string filePath, Dictionary<string, string> keyVars)
        {
            string textInfo = SafeFileManagement.GetFileContents(filePath);
            foreach (string key in keyVars.Keys)
            {
                textInfo = textInfo.Replace(key, keyVars[key]);
            }

            SafeFileManagement.SetFileContents(filePath, textInfo);
        }
    }
}
