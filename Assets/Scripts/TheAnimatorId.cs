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

	public int Complete { get { return impl.Complete; } }
	public int DidFix { get { return impl.DidFix; } }
	public int Hide { get { return impl.Hide; } }
	public int Normal { get { return impl.Normal; } }
	public int Show { get { return impl.Show; } }
	public int ShrinikDown { get { return impl.ShrinikDown; } }
	public int ShrinikUp { get { return impl.ShrinikUp; } }
	public int StartScrolling { get { return impl.StartScrolling; } }

	partial class Impl
	{

		public int Complete;
		public int DidFix;
		public int Hide;
		public int Normal;
		public int Show;
		public int ShrinikDown;
		public int ShrinikUp;
		public int StartScrolling;

		public Impl()
		{
			Complete = Animator.StringToHash("Complete");
			DidFix = Animator.StringToHash("DidFix");
			Hide = Animator.StringToHash("Hide");
			Normal = Animator.StringToHash("Normal");
			Show = Animator.StringToHash("Show");
			ShrinikDown = Animator.StringToHash("ShrinikDown");
			ShrinikUp = Animator.StringToHash("ShrinikUp");
			StartScrolling = Animator.StringToHash("StartScrolling");
		}

	}

}
