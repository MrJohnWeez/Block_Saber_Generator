﻿// Created by MrJohnWeez
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
	/// Open MrJohnWeez Website
	/// </summary>
    public void ToMrJohnWeezHelpSite()
	{
		Application.OpenURL("https://www.mrjohnweez.com/blocksaber.html");
	}

	public void ToMrJohnWeezSite()
	{
		Application.OpenURL("https://www.mrjohnweez.com/");
	}
}
