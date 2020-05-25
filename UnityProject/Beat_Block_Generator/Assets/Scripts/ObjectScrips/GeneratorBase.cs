using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


/// <summary>
/// Base class that handles folder generation for minecraft resource and data packs
/// </summary>
public class GeneratorBase
{
	protected static readonly string C_StreamingAssets = Path.Combine(Application.dataPath, "StreamingAssets");
	protected readonly string[] _excludeExtensions = { ".meta" };
	protected const int C_numberOfIORetryAttempts = 5;

	// Extension types
	protected const string C_Zip = ".zip";

	// File Names
	protected const string C_PackMeta = "pack.mcmeta";

	// Folder Names
	protected const string C_Minecraft = "minecraft";

	// Paths
	protected string _unzippedFolderPath = "";
	protected string _outputPath = "";
	protected string _fullOutputPath = "";
	protected string _rootFolderPath = "";

	protected Dictionary<string, string> _keyVars = new Dictionary<string, string>();
	protected string _packName = "";
	protected PackInfo _packInfo;
	protected string _folder_uuid = "";

	public virtual bool Generate()
	{
		return false;
	}

	/// <summary>
	/// Set up varibles and paths based on class data
	/// </summary>
	protected virtual void Init()
	{
		
	}

	/// <summary>
	/// Copy a directory
	/// </summary>
	/// <param name="sourceDirName">Source folder path to copy</param>
	/// <param name="destDirName">Path to copy to</param>
	/// <returns></returns>
	protected virtual bool CopyTemplate(string sourceDirName, string destDirName)
	{
		return SafeFileManagement.DirectoryCopy(sourceDirName, destDirName, true, _excludeExtensions, C_numberOfIORetryAttempts);
	}
	

	/// <summary>
	/// Create an archive and move to output
	/// </summary>
	/// <param name="sourcePath">Folder to compress</param>
	/// <param name="outputFullPath">Output full path file</param>
	protected void CreateArchive(string sourcePath, string outputFullPath)
	{
		Archive.Compress(sourcePath, outputFullPath);
	}

	/// <summary>
	/// Replace any keys within a file from a dictionary
	/// </summary>
	/// <param name="filePath">path of file to replace keys in</param>
	protected void UpdateFileWithKeys(string filePath)
	{
		string textInfo = SafeFileManagement.GetFileContents(filePath);
		foreach (string key in _keyVars.Keys)
		{
			textInfo = textInfo.Replace(key, _keyVars[key]);
		}

		SafeFileManagement.SetFileContents(filePath, textInfo);
	}
}
