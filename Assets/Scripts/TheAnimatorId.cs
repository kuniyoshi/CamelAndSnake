using System.Collections;
using UnityEngine;

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

	public int Show { get { return impl.Show; } }
	public int Hide { get { return impl.Hide; } }

	partial class Impl
	{

		public int Show;
		public int Hide;

		public Impl()
		{
			Show = Animator.StringToHash("Show");
			Hide = Animator.StringToHash("Hide");
		}

	}

}
