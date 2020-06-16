using System.Collections;
using UnityEngine;

public class FileReceiver : MonoBehaviour
{
	public delegate void FileReceiverString(string url);
	public static event FileReceiverString UserDroppedFile;

	public void FileSelected(string url)
	{
		UserDroppedFile?.Invoke(url);
	}

	// Example usage:
	//private IEnumerator LoadFile(string url)
	//{
	//	var www = new WWW(url);
	//	yield return www;

	//	Url.text = url;
	//	Size.text = www.bytes.Length.ToString();
	//	Error.text = www.error;
	//}
}
