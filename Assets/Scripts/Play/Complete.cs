using System.Collections;
using UnityEngine;

namespace Play
{

public class Complete : MonoBehaviour
{

	static int TheFinishTrigger;
	static int TheCompleteState;
	static int TheDoNotSparkBool;

	public GameObject completeText;

	GameObject sibling;
	Animator surfaceAnimator;
	Animator siblingAnimator;

	public void Animate()
	{
		surfaceAnimator.SetTrigger (TheFinishTrigger);
		siblingAnimator.SetTrigger (TheFinishTrigger);
	}

	public bool DidComplete()
	{
		return surfaceAnimator.GetCurrentAnimatorStateInfo (0).shortNameHash == TheCompleteState;
	}

	void Awake()
	{
		TheFinishTrigger = Animator.StringToHash ("Finish");
		TheCompleteState = Animator.StringToHash ("Complete");
		TheDoNotSparkBool = Animator.StringToHash ("DoNotSpark");

		Debug.Assert (completeText);
		sibling = Instantiate (completeText);
		sibling.name = "sibling";
		sibling.transform.SetParent (transform);

		surfaceAnimator = completeText.GetComponent<Animator> ();
		Debug.Assert (surfaceAnimator);
		Debug.Assert (Test.Util.HasAnimatorParameter (surfaceAnimator, TheFinishTrigger));
		Debug.Assert (surfaceAnimator.HasState (0, TheCompleteState));

		siblingAnimator = sibling.GetComponent<Animator> ();
		Debug.Assert (siblingAnimator);
		Debug.Assert (Test.Util.HasAnimatorParameter (siblingAnimator, TheDoNotSparkBool));
		siblingAnimator.SetBool (TheDoNotSparkBool, true);
	}

}

}
