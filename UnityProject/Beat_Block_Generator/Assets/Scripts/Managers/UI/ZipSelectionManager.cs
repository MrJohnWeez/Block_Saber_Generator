using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using SFB;
using UnityEngine.Networking;
using System.Runtime.InteropServices;

public class ZipSelectionManager : MonoBehaviour, IPointerDownHandler
{
	[SerializeField] private NonConvertedListManager _nonConvertedList = null;
	[SerializeField] private ConvertedListManager _convertedList = null;

#if UNITY_WEBGL && !UNITY_EDITOR
    //
    // WebGL
    //
    [DllImport("__Internal")]
    private static extern void UploadFile(string gameObjectName, string methodName, string filter, bool multiple);

    public void OnPointerDown(PointerEventData eventData) {
        UploadFile(gameObject.name, "OnFileUpload", ".zip", true);
    }

    // Called from browser
    public void OnFileUpload(string urls) {
        StartCoroutine(OutputRoutine(urls.Split(',')));
    }
#else
	//
	// Standalone platforms & editor
	//
	public void OnPointerDown(PointerEventData eventData) { }

	void Start()
	{
		var button = GetComponent<Button>();
		button.onClick.AddListener(OnClick);
	}

	private void OnClick()
	{
		string[] paths = StandaloneFileBrowser.OpenFilePanel("Select Beat Saber Zip", "", "zip", true);
		if (paths.Length > 0)
		{
			List<string> urlArr = new List<string>(paths.Length);
			for (int i = 0; i < paths.Length; i++)
			{
				urlArr.Add(new System.Uri(paths[i]).AbsoluteUri);
			}
			StartCoroutine(OutputRoutine(urlArr.ToArray()));
		}
	}
#endif

	private IEnumerator OutputRoutine(string[] urlArr)
	{
		for (int i = 0; i < urlArr.Length; i++)
		{
			Debug.Log("URL: " + urlArr[i]);
			UnityWebRequest webRequest = new UnityWebRequest(urlArr[i]);
			//WWW test = new WWW(urlArr[i])
			yield return webRequest;
			_nonConvertedList.AddZip(webRequest);
		}
	}
}
