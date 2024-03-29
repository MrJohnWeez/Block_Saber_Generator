﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using SFB;
using UnityEngine;

namespace Utilities.Wrappers
{
    /// <summary>
    /// Wraps most of native function calls to give error handling or more functionally using less functions
    /// </summary>
    public class SafeFileManagement
    {
        #region FileManagement
        /// <summary>
        /// Deletes a file and returns wether it was successful or not
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool DeleteFile(string path)
        {
            File.Delete(path);
            return !File.Exists(path);
        }

        /// <summary>
        /// Get a file's entire contents
        /// </summary>
        /// <param name="filePath">Path of a file to get contents</param>
        /// <returns>Entire file as one string or null if error</returns>
        public static string GetFileContents(string filePath)
        {
            string returnThis = "";
            try
            {
                using (var sr = new StreamReader(filePath))
                {
                    returnThis = sr.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                Debug.Log("The file could not be read:\nError: \n" + e.Message.ToString());
                returnThis = null;
            }
            return returnThis;
        }

        /// <summary>
        /// Sets a file's contents to a given string
        /// </summary>
        /// <param name="filePath">Path of a file to set</param>
        /// <param name="content">The new contents of an entire file</param>
        /// <returns>True if successful</returns>
        public static bool SetFileContents(string filePath, string content)
        {
            bool wasSuccessful = false;
            try
            {
                using (var sw = new StreamWriter(filePath))
                {
                    sw.WriteLine(content);
                }
                wasSuccessful = true;
            }
            catch (Exception e)
            {
                Debug.Log("The file could not be written to:\nError: \n" + e.Message.ToString());
            }
            return wasSuccessful;
        }

        /// <summary>
        /// Get the name of a file before extension
        /// </summary>
        /// <param name="inFileName">The name of the file including extension</param>
        /// <returns>Name of a file without extension name</returns>
        public static string GetFileName(string inFileName)
        {
            int index = inFileName.LastIndexOf(".");
            if (index > 0)
            {
                return inFileName[..index];
            }
            return inFileName;
        }

        /// <summary>
        /// Get Files within given directory
        /// </summary>
        /// <param name="sourceDirName">Source directory</param>
        /// <param name="retryAttempts">Number of attempts to retry get</param>
        /// <returns>FileInfo array and Null if errors</returns>
        public static FileInfo[] GetFilesInfo(string sourceDirName, int retryAttempts = 0)
        {
            FileInfo[] files = null;
            if (Directory.Exists(sourceDirName))
            {
                DirectoryInfo dir = new DirectoryInfo(sourceDirName);

                int currAttempts = 0;
                bool didMove = false;
                while (!didMove && currAttempts <= retryAttempts)
                {
                    try
                    {
                        files = dir.GetFiles();
                        didMove = true;
                    }
                    catch (UnauthorizedAccessException accessDenied)
                    {
                        currAttempts++;
                        Debug.Log(accessDenied.Message);
                        Thread.Sleep(50);   // Wait for 50ms before checking again
                        continue;
                    }
                    catch (Exception e)
                    {
                        Debug.Log(e.Message);
                        currAttempts = retryAttempts + 1;
                    }
                }
            }
            return files;
        }

        /// <summary>
        /// Get File paths within a directory
        /// </summary>
        /// <param name="sourceDirName">Source directory</param>
        /// <param name="retryAttempts">Number of attempts to retry get</param>
        /// <returns>Full name paths string array and Empty list if error</returns>
        public static string[] GetFilesPaths(string sourceDirName, int retryAttempts = 0)
        {
            FileInfo[] files = GetFilesInfo(sourceDirName, retryAttempts);
            List<string> fileNameList = new List<string>();
            foreach (FileInfo file in files)
            {
                fileNameList.Add(file.FullName);
            }
            return fileNameList.ToArray();
        }

        /// <summary>
        /// Get directory paths within a directory
        /// </summary>
        /// <param name="sourceDirName">Source directory</param>
        /// <param name="retryAttempts">Number of attempts to retry get</param>
        /// <returns>Full name paths string array and Empty list if error</returns>
        public static string[] GetDirectoryPaths(string sourceDirName, int retryAttempts = 0)
        {
            DirectoryInfo[] directories = GetDirectoryInfo(sourceDirName, retryAttempts);
            List<string> dirNameList = new List<string>();
            if (directories != null && directories.Length > 0)
            {
                foreach (DirectoryInfo dir in directories)
                {
                    dirNameList.Add(dir.FullName);
                }
                return dirNameList.ToArray();
            }
            return new string[0];
        }

        /// <summary>
        /// Safely copies a file to a new destination
        /// </summary>
        /// <param name="fileInfo">Source file info</param>
        /// <param name="destFileName">Destination file path</param>
        /// <param name="overWriteFile">Should file be overwitten</param>
        /// <param name="retryAttempts">Number of attempts to retry move</param>
        /// <returns>True if successful</returns>
        public static bool CopyFileTo(FileInfo fileInfo, string destFileName, bool overWriteFile = false, int retryAttempts = 0)
        {
            int currAttempts = 0;
            bool didCopy = false;
            while (!didCopy && currAttempts <= retryAttempts)
            {
                try
                {
                    fileInfo.CopyTo(destFileName, overWriteFile);
                    didCopy = true;
                }
                catch (UnauthorizedAccessException accessDenied)
                {
                    currAttempts++;
                    Debug.Log(accessDenied.Message);
                    Thread.Sleep(50);   // Wait for 50ms before checking again
                    continue;
                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                    currAttempts = retryAttempts + 1;
                }
            }

            return File.Exists(destFileName);
        }

        /// <summary>
        /// Safely copies a file to a new destination
        /// </summary>
        /// <param name="fromFileName">Source file path</param>
        /// <param name="destFileName">Destination file path</param>
        /// <param name="overWriteFile">Should file be overwitten</param>
        /// <param name="retryAttempts">Number of attempt to retry move</param>
        /// <returns>True if successful</returns>
        public static bool CopyFileTo(string fromFileName, string destFileName, bool overWriteFile = false, int retryAttempts = 0)
        {
            return CopyFileTo(new FileInfo(fromFileName), destFileName, overWriteFile, retryAttempts);
        }

        /// <summary>
        /// Safely moves a file to a new destination
        /// </summary>
        /// <param name="sourceDirName">Source File</param>
        /// <param name="destDirName">Destination file</param>
        /// <param name="retryAttempts">Number of attempts to retry move</param>
        /// <returns>True if successful</returns>
        public static bool MoveFile(string sourceFileName, string destFileName, int retryAttempts = 0)
        {
            if (File.Exists(sourceFileName))
            {
                int currAttempts = 0;
                bool didMove = false;
                while (!didMove && currAttempts <= retryAttempts)
                {
                    try
                    {
                        File.Move(sourceFileName, destFileName);
                        didMove = true;
                    }
                    catch (UnauthorizedAccessException accessDenied)
                    {
                        currAttempts++;
                        Debug.Log(accessDenied.Message);
                        Thread.Sleep(50);   // Wait for 50ms before checking again
                        continue;
                    }
                    catch (Exception e)
                    {
                        Debug.Log(e.Message);
                        currAttempts = retryAttempts + 1;
                    }
                }
            }
            return File.Exists(destFileName);
        }

        public static bool AppendFile(string filePath, string content)
        {
            bool wasSuccessful = false;
            try
            {
                using (var sw = new StreamWriter(filePath, true))
                {
                    sw.WriteLine(content);
                }
                wasSuccessful = true;
            }
            catch (Exception e)
            {
                Debug.Log("The file could not be appended to:\nError: \n" + e.Message.ToString());
            }
            return wasSuccessful;
        }

        #endregion FileManagement

        #region DirecotryManagement
        /// <summary>
        /// Get Directories within given directory
        /// </summary>
        /// <param name="sourceDirName">Source directory</param>
        /// <param name="retryAttempts">Number of attempts to retry get</param>
        /// <returns>FileInfo array and Null if errors</returns>
        public static DirectoryInfo[] GetDirectoryInfo(string sourceDirName, int retryAttempts = 0)
        {
            DirectoryInfo[] files = null;
            if (Directory.Exists(sourceDirName))
            {
                DirectoryInfo dir = new DirectoryInfo(sourceDirName);

                int currAttempts = 0;
                bool didMove = false;
                while (!didMove && currAttempts <= retryAttempts)
                {
                    try
                    {
                        files = dir.GetDirectories();
                        didMove = true;
                    }
                    catch (UnauthorizedAccessException accessDenied)
                    {
                        currAttempts++;
                        Debug.Log(accessDenied.Message);
                        Thread.Sleep(50);   // Wait for 50ms before checking again
                        continue;
                    }
                    catch (Exception e)
                    {
                        Debug.Log(e.Message);
                        currAttempts = retryAttempts + 1;
                    }
                }
            }
            return files;
        }

        /// <summary>
        /// Opens a native file browers and returns a path selected
        /// </summary>
        /// <param name="dialogTitle">What to display on the window popup</param>
        /// <param name="startingFolder">The folder to start user in</param>
        /// <returns></returns>
        public static string FolderPath(string dialogTitle, string startingFolder = "")
        {
            string[] selectedRootFolder = StandaloneFileBrowser.OpenFolderPanel(dialogTitle, startingFolder, false);

            if (selectedRootFolder.Length > 0)
            {
                return selectedRootFolder[0];
            }
            return "";
        }

        /// <summary>
        /// Get lowest folder name within the given path
        /// </summary>
        /// <param name="inFolderPath">Path of a folder</param>
        /// <returns>Lowest folder name</returns>
        public static string GetFolderName(string inFolderPath)
        {
            string fullPath = Path.GetFullPath(GetFileName(inFolderPath)).TrimEnd(Path.DirectorySeparatorChar);
            string projectName = Path.GetFileName(fullPath);
            return projectName;
        }

        /// <summary>
        /// Copy entire directory to a different location
        /// </summary>
        /// <param name="sourceDirName">The folder to copy files from</param>
        /// <param name="destDirName">The location to copy files to</param>
        /// <param name="copySubDirs">Should sub files and folders be copied</param>
        /// <param name="excludeExtensions">String array of any file extensions to not copy</param>
        /// <param name="retryAttempts">Number of retried system will preform if errors are encountered</param>
        /// <returns>True if successful</returns>
        public static bool DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs, string[] excludeExtensions = null, int retryAttempts = 0)
        {
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }
            DirectoryInfo[] dirs = GetDirectories(sourceDirName, retryAttempts);
            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs && dirs != null && dirs.Length > 0)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    if (!DirectoryCopy(subdir.FullName, temppath, copySubDirs, excludeExtensions, retryAttempts))
                    {
                        Debug.LogError("Failed to copy sub-directory " + subdir.FullName + " to " + temppath);
                        return false;
                    }
                }
            }

            FileInfo[] files = GetFilesInfo(sourceDirName, retryAttempts);
            if (files != null && files.Length > 0)
            {
                foreach (FileInfo file in files)
                {
                    string temppath = Path.Combine(destDirName, file.Name);
                    bool skipThisFile = false;
                    if (excludeExtensions != null)
                    {
                        foreach (string ext in excludeExtensions)
                        {
                            skipThisFile = ext == Path.GetExtension(temppath);
                        }
                    }

                    if (!skipThisFile)
                    {
                        if (!CopyFileTo(file, temppath, true, retryAttempts))
                        {
                            Debug.LogError("Failed to copy file " + file.Name + " to " + temppath);
                            return false;
                        }
                    }

                }
            }
            return true;
        }

        /// <summary>
        /// Get Directories within given path
        /// </summary>
        /// <param name="sourceDirName">Source direcotry</param>
        /// <param name="retryAttempts">Number of attempts to retry get</param>
        /// <returns>DirectoryInfo array and Null if errors</returns>
        public static DirectoryInfo[] GetDirectories(string sourceDirName, int retryAttempts = 0)
        {
            DirectoryInfo[] dirs = null;
            if (Directory.Exists(sourceDirName))
            {
                DirectoryInfo dir = new DirectoryInfo(sourceDirName);

                int currAttempts = 0;
                bool didMove = false;
                while (!didMove && currAttempts <= retryAttempts)
                {
                    try
                    {
                        dirs = dir.GetDirectories();
                        didMove = true;
                    }
                    catch (UnauthorizedAccessException accessDenied)
                    {
                        currAttempts++;
                        Debug.Log(accessDenied.Message);
                        Thread.Sleep(50);   // Wait for 50ms before checking again
                        continue;
                    }
                    catch (Exception e)
                    {
                        Debug.Log(e.Message);
                        currAttempts = retryAttempts + 1;
                    }
                }
            }
            return dirs;
        }

        /// <summary>
        /// Safely moves a directory to a new destination
        /// </summary>
        /// <param name="sourceDirName">Source Directory</param>
        /// <param name="destDirName">Destination directory</param>
        /// <param name="retryAttempts">Number of attempts to retry move</param>
        /// <returns>True if successful</returns>
        public static bool MoveDirectory(string sourceDirName, string destDirName, int retryAttempts = 0)
        {
            if (Directory.Exists(sourceDirName))
            {
                int currAttempts = 0;
                bool didMove = false;
                while (!didMove && currAttempts <= retryAttempts)
                {
                    try
                    {
                        Directory.Move(sourceDirName, destDirName);
                        didMove = true;
                    }
                    catch (UnauthorizedAccessException accessDenied)
                    {
                        currAttempts++;
                        Debug.Log(accessDenied.Message);
                        Thread.Sleep(50);   // Wait for 50ms before checking again
                        continue;
                    }
                    catch (Exception e)
                    {
                        Debug.Log(e.Message);
                        currAttempts = retryAttempts + 1;
                    }
                }
            }

            return Directory.Exists(destDirName);
        }

        public static void DeleteDirectory(string directoryPath)
        {
            Directory.Delete(directoryPath, true);
        }

        public static bool OpenFolder(string path)
        {
            if (Directory.Exists(path))
            {
                System.Diagnostics.Process.Start(path);
                return true;
            }
            return false;
        }
        #endregion DirecotryManagement

        /// <summary>
        /// Returns the given date
        /// </summary>
        /// <returns>Year as string</returns>
        public static string GetDateNow()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmss");
        }
    }
}