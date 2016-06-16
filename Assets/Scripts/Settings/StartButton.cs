using System.Collections;
using UnityEngine;

public class StartButton : MonoBehaviour
{

	Animator animator;
	int ThePushedTrigger;
	int TheReadyToNextState;
	int TheNormalTrigger;
	OnTinyEvent onReadyToNext;

	public void FlushDeflation()
	{
		animator.SetTrigger (ThePushedTrigger);
	}

	public void Subscribe(OnTinyEvent newOne)
	{
		onReadyToNext += newOne;
	}

	void Start()
	{
		animator = GetComponent<Animator> ();
		Debug.Assert (animator);

		ThePushedTrigger = Animator.StringToHash ("Pressed");
		Debug.Assert (Test.Util.HasAnimatorParamter (animator, ThePushedTrigger));

		TheReadyToNextState = Animator.StringToHash ("ReadyToNext");
		Debug.Assert (animator.HasState (0, TheReadyToNextState));

		TheNormalTrigger = Animator.StringToHash ("Normal");
		Debug.Assert (Test.Util.HasAnimatorParamter (animator, TheNormalTrigger));
	}

	void Update()
	{
		if (animator.GetCurrentAnimatorStateInfo(0).shortNameHash == TheReadyToNextState)
		{
			NotifySubscriber ();
			animator.SetTrigger (TheNormalTrigger);
		}
	}

	void NotifySubscriber()
	{
		if (onReadyToNext != null)
		{
			onReadyToNext ();
		}
	}

}
