using System.Collections;
using UnityEngine;

namespace Result
{

public class WhoWon : MonoBehaviour
{

	enum State
	{
		Hiding,
		Screwing,
		LightingBegin,
		Lighting,
		Completed,
	}

	public float LightingDuration = 3f;
	public SearchLight searchLight;
	public Animator whoWonAnimator;

	State currentState;
	float time;

	public bool DidComplete()
	{
		return currentState == State.Completed;
	}

	public void Show()
	{
		whoWonAnimator.SetTrigger (TheAnimatorId.Instance ().Show);
		currentState = State.Screwing;
	}

	void Awake()
	{
		Debug.Assert (searchLight);
		Debug.Assert (whoWonAnimator);
	}

	void Start()
	{
		Test.Util.HasAnimatorParameter (whoWonAnimator, TheAnimatorId.Instance ().Show);
		Debug.Assert (whoWonAnimator.HasState (0, TheAnimatorId.Instance ().Complete));

		currentState = State.Hiding;
	}

	void Update()
	{
		switch (currentState)
		{
		case State.Hiding:
			return;
			break;
		case State.Screwing:
			if (whoWonAnimator.GetCurrentAnimatorStateInfo (0).shortNameHash == TheAnimatorId.Instance ().Complete)
			{
				currentState = State.LightingBegin;
			}
			break;
		case State.LightingBegin:
			currentState = State.Lighting;
			time = Time.time;
			searchLight.Show ();
			break;
		case State.Lighting:
			if (Time.time - time > LightingDuration)
			{
				currentState = State.Completed;
			}
			break;
		case State.Completed:
			break;
		}
	}

}

}
