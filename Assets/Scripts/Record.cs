using System.Collections.Generic;
using UnityEngine;

public class Record
{

	static string Prefix = "Record.";

	public int CamelCount { get; set; }

	public float CamelTotalTime { get; set; }

	public int SnakeCount { get; set; }

	public float SnakeTotalTime { get; set; }

	public void Save()
	{
		PlayerPrefs.SetInt (Prefix + "CamelCount", CamelCount);
		PlayerPrefs.SetFloat (Prefix + "CamelTotalTime", CamelTotalTime);
		PlayerPrefs.SetInt (Prefix + "SnakeCount", SnakeCount);
		PlayerPrefs.SetFloat (Prefix + "SnakeTotalTime", SnakeTotalTime);
	}

	public void Load()
	{
		CamelCount = PlayerPrefs.GetInt (Prefix + "CamelCount");
		CamelTotalTime = PlayerPrefs.GetFloat (Prefix + "CamelTotalTime");
		SnakeCount = PlayerPrefs.GetInt (Prefix + "SnakeCount");
		SnakeTotalTime = PlayerPrefs.GetFloat (Prefix + "SnakeTotalTime");
	}

}
