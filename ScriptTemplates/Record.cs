using System.Collections.Generic;
using UnityEngine;

[%- # CARE BOM EXISTENCE -%]
[% SET fields => [
	{ type => "int", name => "CamelCount" },
	{ type => "int", name => "CamelWordCount" },
	{ type => "float", name => "CamelTotalTime" },
	{ type => "int", name => "SnakeCount" },
	{ type => "int", name => "SnakeWordCount" },
	{ type => "float", name => "SnakeTotalTime" },
] -%]

public class Record
{

	static string Prefix = "Record.";

[%- FOREACH field IN fields -%]

	public [% GET field.type %] [% GET field.name %] { get; set; }
[% END # FOREACH field IN fields -%]

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
