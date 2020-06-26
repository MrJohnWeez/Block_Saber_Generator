using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class SelectionManager : MonoBehaviour
{
	private Animator _dragAndDrop = null;

	private void Start()
	{
		_dragAndDrop = GetComponent<Animator>();
	}

	public void PointerEnter()
	{
		_dragAndDrop.SetBool("PointerEntered", true);
	}

	public void PointerExit()
	{
		_dragAndDrop.SetBool("PointerEntered", false);
	}

	public void PointerClicked()
	{
		Debug.Log("Clicked");
	}
}
