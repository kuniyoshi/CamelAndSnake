using System.Collections;
using UnityEngine;

// *****************************************************
// *** This file will be baked from ScriptTemplates. ***
// *****************************************************

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

	public int Hide { get { return impl.Hide; } }
	public int Show { get { return impl.Show; } }

	partial class Impl
	{

		public int Hide;
		public int Show;

		public Impl()
		{
			Hide = Animator.StringToHash("Hide");
			Show = Animator.StringToHash("Show");
		}

	}

}
