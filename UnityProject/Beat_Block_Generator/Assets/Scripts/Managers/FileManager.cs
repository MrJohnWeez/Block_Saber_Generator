using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using SFB;
using System.IO;

public class FileManager : MonoBehaviour
{
	private readonly ExtensionFilter[] extensions = { new ExtensionFilter("Beat Saber Song Pack", "zip") };

	[Header("Texts")]
	[SerializeField] private TMP_Text _zipFilePathText = null;
	[SerializeField] private TMP_Text _datapackOutputPathText = null;

	[SerializeField] private Button GenerateDatapackButton = null;

	private string _zipFilePath = "";
	private string _datapackOutputPath = "";
	private Converter _converter = null;

	#region UnityCallbacks
	private void Start()
	{
		//_zipFilePath = "C:\\Users\\John\\Desktop\\72808dcf1d5e2f3b4d08ba98cec32d39d696ba91.zip";
		_datapackOutputPath = "C:\\Users\\John\\Desktop";
		AreSelectedPathsValid();
		//ConvertAndCreateResources();
	}

	#endregion UnityCallbacks


	#region FileSelection
	/// <summary>
	/// Lets the user select a zip file to be parsed and converted
	/// </summary>
	public void SelectZipFile()
	{
		string[] zipFile = StandaloneFileBrowser.OpenFilePanel("Select Beat Saber Song Zip", "", extensions, false);
		string newPath = zipFile.Length > 0 ? zipFile[0] : "";
		if (!newPath.IsEmpty())
		{
			_zipFilePath = newPath;
			_zipFilePathText.text = _zipFilePath;
			AreSelectedPathsValid();
		}
	}

	/// <summary>
	/// Lets the user select the datapack output folder
	/// </summary>
	public void SelectDatapackOutputPath()
	{
		string newPath = SafeFileManagement.FolderPath("Select where datapack will be saved");
		if (!newPath.IsEmpty())
		{
			_datapackOutputPath = newPath;
			_datapackOutputPathText.text = _datapackOutputPath;
			AreSelectedPathsValid();
		}
	}

	/// <summary>
	/// Convert and generate a datapack if the input and output paths are valid
	/// </summary>
	public void ConvertAndCreateResources()
	{
		if (AreSelectedPathsValid())
		{
			_converter = new Converter(_zipFilePath, _datapackOutputPath);
			_converter.GenerateMinecraftResources();
		}
	}

	#endregion FileSelection

	/// <summary>
	/// Acts like a validation function for when the user is allowed to generate the datapack
	/// </summary>
	/// <returns>True if user can generate datapack (paths are valid)</returns>
	private bool AreSelectedPathsValid()
	{
		bool areValid = File.Exists(_zipFilePath) && Directory.Exists(_datapackOutputPath);
		GenerateDatapackButton.interactable = areValid;
		return areValid;
	}

}
