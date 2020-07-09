using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace MrJohnWeez
{
	[RequireComponent(typeof(TMP_Text))]
	public class VersionTMPText : MonoBehaviour
	{
		private TMP_Text textObject = null;
		void Start()
		{
			textObject = GetComponent<TMP_Text>();
			textObject.text = "v" + Application.version;
		}
	}
}