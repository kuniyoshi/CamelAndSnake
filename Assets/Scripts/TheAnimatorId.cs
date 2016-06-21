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
	public int Normal { get { return impl.Normal; } }
	public int Show { get { return impl.Show; } }
	public int ShrinikDown { get { return impl.ShrinikDown; } }
	public int ShrinikUp { get { return impl.ShrinikUp; } }

	partial class Impl
	{

		public int Hide;
		public int Normal;
		public int Show;
		public int ShrinikDown;
		public int ShrinikUp;

		public Impl()
		{
			Hide = Animator.StringToHash("Hide");
			Normal = Animator.StringToHash("Normal");
			Show = Animator.StringToHash("Show");
			ShrinikDown = Animator.StringToHash("ShrinikDown");
			ShrinikUp = Animator.StringToHash("ShrinikUp");
		}

	}

}
