using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Title
{

public class TouchToStart : MonoBehaviour
{
	Animator animator;
	OnTinyEvent onTinyEvent;

	public void InflateOnce()
	{
		animator.SetTrigger (TheAnimatorId.Instance ().OnSceneChange);
	}

	public void Subscribe(OnTinyEvent newOne)
	{
		onTinyEvent += newOne;
	}

	void Start()
	{
		animator = GetComponentInParent<Animator> ();
		Debug.Assert (animator);
		Debug.Assert (Test.Util.HasAnimatorParameter (animator, TheAnimatorId.Instance (true).OnSceneChange));
		Debug.Assert (animator.HasState (0, TheAnimatorId.Instance ().TheLastState));
	}

	void Update()
	{
		AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo (0);

		if (info.shortNameHash == TheAnimatorId.Instance ().TheLastState)
		{

			if (onTinyEvent != null)
			{
				onTinyEvent ();
			}
		}
	}

}

}
