using System.Collections.Generic;
using UnityEngine;

[%- # CARE BOM EXISTENCE -%]
[% SET fields => [
	{ type => "int", name => "CamelLetters" },
	{ type => "int", name => "CamelWords" },
	{ type => "int", name => "CamelPhrases" },
	{ type => "float", name => "CamelTotalTime" },
	{ type => "int", name => "SnakeLetters" },
	{ type => "int", name => "SnakeWors" },
	{ type => "int", name => "SnakePhrases" },
	{ type => "float", name => "SnakeTotalTime" },
] -%]

public class Record
{

	static string Prefix = "Record.";

[%- FOREACH field IN fields -%]

	public [% GET field.type %] [% GET field.name %] { get; set; }
[% END # FOREACH field IN fields -%]

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
[% FOREACH field IN fields -%]
		PlayerPrefs.Set[% GET field.type.ucfirst -%] (Prefix + "[% GET field.name %]", [% GET field.name %]);
[% END # FOREACH field IN fields -%]
	}

	public void Load()
	{
[% FOREACH field IN fields -%]
		[% field.name %] = PlayerPrefs.Get[% field.type.ucfirst %] (Prefix + "[% GET field.name %]");
[% END # FOREACH field IN fields -%]
	}

}
