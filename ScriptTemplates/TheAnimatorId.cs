using System.Collections;
using UnityEngine;

[%- # CARE BOM EXISTENCE -%]
[% SET keywords => [
	"Hide",
	"Show",
] -%]

public class TheAnimatorId
{

	partial class Impl {}

	static Impl impl;

	public static void Create()
	{
		Debug.Assert (impl == null);
		impl = new Impl();
	}

	public static void Destroy()
	{
		Debug.Assert (impl != null);
		impl = null;
	}

	public static TheAnimatorId Instance() { return new TheAnimatorId(); }

	TheAnimatorId() {} // hide from public

[% FOREACH keyword IN keywords -%]
	public int [% GET keyword %] { get { return impl.[% GET keyword %]; } }
[% END # FOREACH keyword IN keywords -%]

	partial class Impl
	{

[% FOREACH keyword IN keywords -%]
		public int [% GET keyword %];
[% END # FOREACH keyword IN keywords -%]

		public Impl()
		{
[% FOREACH keyword IN keywords -%]
			[% GET keyword %] = Animator.StringToHash("[% GET keyword %]");
[% END # FOREACH keyword IN keywords -%]
		}

	}

}
