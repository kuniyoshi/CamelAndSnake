﻿using System.Collections;
using UnityEngine;

public struct Configuration
{
	public enum CamelType
	{
		Pascal,
		Camel,
		Mix,
	}

	public enum LetterCase
	{
		Camel,
		Snake,
		Shuffle,
	}

	public CamelType camelType;
	public LetterCase order;
	public int times;

	public bool DidConfigure() { return times != 0; }

	public override string ToString()
	{
		return "Configuration {"
			+ "camel type: " + camelType + ", "
			+ "letter case: " + order + ", "
			+ "times: " + times
			+ "}";
	}

}
