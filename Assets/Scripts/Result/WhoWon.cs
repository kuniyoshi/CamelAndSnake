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
		Lighting,
		Completed,
	}

	public SearchLight searchLight;
	public Animator whoWonAnimator;

	State currentState;

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
				currentState = State.Lighting;
			}
			break;
		case State.Lighting:
			searchLight.Show ();
			break;
		case State.Completed:
			break;
		}
	}

}

}
