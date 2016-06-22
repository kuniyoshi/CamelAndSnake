using System.Collections;
using UnityEngine;

[%- # CARE BOM EXISTENCE -%]
[% SET keywords => [
	"Complete",
	"DidFix",
	"Hide",
	"Normal",
	"Show",
	"ShrinikDown",
	"ShrinikUp",
	"StartScrolling",
] -%]

public class TheAnimatorId
{

	partial class Impl {}

	static Impl impl;

	public static void Create()
	{
		if (impl != null)
		{
			return;
		}

		impl = new Impl();
	}

	public static void Destroy()
	{
		Debug.Assert (impl != null);
		impl = null;
	}

	public static TheAnimatorId Instance() { return new TheAnimatorId(); }

	public static TheAnimatorId Instance(bool createIfNeeded)
	{
		if (impl == null)
		{
			Create ();
		}

		return Instance ();
	}


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
