using UnityEngine;

namespace Title
{

public class TheScene
{
	public enum State
	{
		Idle,
		StartChanging,
		TrulyChanging,

		Count,
	}

	partial class Impl {}

	static Impl impl;
	static System.EventArgs arg;

	public static void Create()
	{
		Debug.Assert (impl == null);
		impl = new Impl ();
		arg = new System.EventArgs ();
	}

	public static void Destroy()
	{
		Debug.Assert (impl != null);
		impl = null;
	}

	public static TheScene Instance() { return new TheScene (); }

	public static bool HasBeenCreated() { return impl != null; }

	public void Add(State to, OnSceneChange what) { impl.Add(to, what); }
	
	public void Next() { impl.Next (this, arg); }

	public void Complete() { impl.Complete (this, arg); }

	partial class Impl
	{
		DelegateChain sequencer;

		public Impl()
		{
			sequencer = new DelegateChain((int)State.Count);
		}
		
		public void Add(State to, OnSceneChange d)
		{
			sequencer.Add ((int)to, d);
		}
		
		public void Next(object o, System.EventArgs arg)
		{
			sequencer.StepOver (o, arg);
		}

		public void Complete(object o, System.EventArgs arg)
		{
			sequencer.StepOut (o, arg);
		}
	}
}

}
