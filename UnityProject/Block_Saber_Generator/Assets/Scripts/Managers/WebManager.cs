// Created by MrJohnWeez
// March 2020
//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple class that re-directs user to Websites
/// </summary>
public class WebManager: MonoBehaviour
{
	/// <summary>
	/// Open MrJohnWeez Website to Block Saber help page
	/// </summary>
    public void ToMrJohnWeezHelpSite()
	{
		Application.OpenURL("https://www.mrjohnweez.com/blocksaber.html");
	}

	/// <summary>
	/// Open MrJohnWeez Website main page
	/// </summary>
	public void ToMrJohnWeezSite()
	{
		Application.OpenURL("https://www.mrjohnweez.com/");
	}

	/// <summary>
	/// Open BeastSaber Website main page
	/// </summary>
	public void ToBeastSaberSite()
	{
		Application.OpenURL("https://bsaber.com/songs/top/");
	}
}
