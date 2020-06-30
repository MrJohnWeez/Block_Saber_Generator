using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Minecraft
{
	public static class Filemanagement
	{
		/// <summary>
		/// Update all files within a directory to correct varible names
		/// </summary>
		/// <param name="folderPath">In folder path</param>
		public static void UpdateAllCopiedFiles(string folderPath, Dictionary<string, string> keyVars, bool checkSubDirectories = false)
		{
			if (checkSubDirectories)
			{
				string[] dirs = SafeFileManagement.GetDirectoryPaths(folderPath, Globals.C_numberOfIORetryAttempts);
				foreach (string dir in dirs)
				{
					UpdateAllCopiedFiles(dir, keyVars, checkSubDirectories);
				}
			}

			if (Directory.Exists(folderPath))
			{
				string[] files = SafeFileManagement.GetFilesPaths(folderPath, Globals.C_numberOfIORetryAttempts);
				foreach (string file in files)
				{
					UpdateFileWithKeys(file, keyVars);
				}
			}
		}

		/// <summary>
		/// Replace any keys within a file from a dictionary
		/// </summary>
		/// <param name="filePath">path of file to replace keys in</param>
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
