using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Title
{

public class TouchToStart : MonoBehaviour
{
	Animator animator;
	OnTinyEvent onTinyEvent;

	int TriggerId = Animator.StringToHash ("OnSceneChange");
	int TheLastState = Animator.StringToHash("TheLastState");

	public void InflateOnce()
	{
		animator.SetTrigger (TriggerId);
	}

	public void Subscribe(OnTinyEvent newOne)
	{
		onTinyEvent += newOne;
	}

//	public void Unsubscribe(OnTinyEvent alreadyOne)
//	{
//		onTinyEvent -= alreadyOne;
//	}

	void Start()
	{
		animator = GetComponentInParent<Animator> ();
		Debug.Assert(animator != null && Test.Util.HasAnimatorParamter (animator, TriggerId));
		Debug.Assert (animator != null && animator.HasState (0, TheLastState));
	}

	void Update()
	{
		if (animator.GetCurrentAnimatorStateInfo(0).shortNameHash == TheLastState)
		{
			if (onTinyEvent != null)
			{
				onTinyEvent ();
			}
		}
	}

}

}
