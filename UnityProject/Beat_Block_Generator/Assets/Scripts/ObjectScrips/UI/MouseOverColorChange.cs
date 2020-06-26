using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionHover : MonoBehaviour
{
	[SerializeField] private GameObject _icons = null;
	[SerializeField] private Image _background = null;

	public void PointerEnter()
	{
		Debug.Log("Enter");
	}

	public void PointerExit()
	{
		Debug.Log("Exit");
	}
}