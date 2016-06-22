using System.Collections.Generic;
using UnityEngine;

// *****************************************************
// *** This file will be baked from ScriptTemplates. ***
// *****************************************************

public class Record
{

	static string Prefix = "Record.";

	public int CamelCount { get; set; }

	public int CamelWordCount { get; set; }

	public float CamelTotalTime { get; set; }

	public int SnakeCount { get; set; }

	public int SnakeWordCount { get; set; }

	public float SnakeTotalTime { get; set; }

	public void Save()
	{
		PlayerPrefs.SetInt (Prefix + "CamelCount", CamelCount);
		PlayerPrefs.SetInt (Prefix + "CamelWordCount", CamelWordCount);
		PlayerPrefs.SetFloat (Prefix + "CamelTotalTime", CamelTotalTime);
		PlayerPrefs.SetInt (Prefix + "SnakeCount", SnakeCount);
		PlayerPrefs.SetInt (Prefix + "SnakeWordCount", SnakeWordCount);
		PlayerPrefs.SetFloat (Prefix + "SnakeTotalTime", SnakeTotalTime);
	}

	public void Load()
	{
		CamelCount = PlayerPrefs.GetInt (Prefix + "CamelCount");
		CamelWordCount = PlayerPrefs.GetInt (Prefix + "CamelWordCount");
		CamelTotalTime = PlayerPrefs.GetFloat (Prefix + "CamelTotalTime");
		SnakeCount = PlayerPrefs.GetInt (Prefix + "SnakeCount");
		SnakeWordCount = PlayerPrefs.GetInt (Prefix + "SnakeWordCount");
		SnakeTotalTime = PlayerPrefs.GetFloat (Prefix + "SnakeTotalTime");
	}

}
