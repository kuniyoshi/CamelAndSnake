using System.Collections.Generic;
using UnityEngine;

// *****************************************************
// *** This file will be baked from ScriptTemplates. ***
// *****************************************************

public class Record
{

	static string Prefix = "Record.";

	public int CamelLetters { get; set; }

	public int CamelWords { get; set; }

	public int CamelPhrases { get; set; }

	public float CamelTotalTime { get; set; }

	public int SnakeLetters { get; set; }

	public int SnakeWors { get; set; }

	public int SnakePhrases { get; set; }

	public float SnakeTotalTime { get; set; }

	public int CamelScore
	{
		get
		{
			float lettersPerSecond = (float)CamelLetters / CamelTotalTime;
			float lettersPerMs = lettersPerSecond * 1000f;
			return (int)(lettersPerMs + 0.5f);
		}
	}

	public int SnakeScore
	{
		get
		{
			float lettersPerSecond = (float)SnakeLetters / SnakeTotalTime;
			float lettersPerMs = lettersPerSecond * 1000f;
			return (int)(lettersPerMs + 0.5f);
		}
	}

	public void Save()
	{
		PlayerPrefs.SetInt (Prefix + "CamelLetters", CamelLetters);
		PlayerPrefs.SetInt (Prefix + "CamelWords", CamelWords);
		PlayerPrefs.SetInt (Prefix + "CamelPhrases", CamelPhrases);
		PlayerPrefs.SetFloat (Prefix + "CamelTotalTime", CamelTotalTime);
		PlayerPrefs.SetInt (Prefix + "SnakeLetters", SnakeLetters);
		PlayerPrefs.SetInt (Prefix + "SnakeWors", SnakeWors);
		PlayerPrefs.SetInt (Prefix + "SnakePhrases", SnakePhrases);
		PlayerPrefs.SetFloat (Prefix + "SnakeTotalTime", SnakeTotalTime);
	}

	public void Load()
	{
		CamelLetters = PlayerPrefs.GetInt (Prefix + "CamelLetters");
		CamelWords = PlayerPrefs.GetInt (Prefix + "CamelWords");
		CamelPhrases = PlayerPrefs.GetInt (Prefix + "CamelPhrases");
		CamelTotalTime = PlayerPrefs.GetFloat (Prefix + "CamelTotalTime");
		SnakeLetters = PlayerPrefs.GetInt (Prefix + "SnakeLetters");
		SnakeWors = PlayerPrefs.GetInt (Prefix + "SnakeWors");
		SnakePhrases = PlayerPrefs.GetInt (Prefix + "SnakePhrases");
		SnakeTotalTime = PlayerPrefs.GetFloat (Prefix + "SnakeTotalTime");
	}

}
