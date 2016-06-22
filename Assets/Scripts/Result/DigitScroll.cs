using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Result
{

public class DigitScroll : MonoBehaviour
{

	static int MinCountToPass = 2;

	int currentDigit;
	int theFinalDigit;
	Text text;
	Animator animator;
	bool tryFix;
	int passCount;

	public void Fix()
	{
		tryFix = true;
		passCount = 0;
	}

	public void IncrementDigit()
	{
		++currentDigit;
		currentDigit = currentDigit % 10;
		text.text = currentDigit.ToString ();

		if (tryFix && currentDigit == theFinalDigit)
		{
			++passCount;
		}

		if (passCount == MinCountToPass)
		{
			animator.SetBool (TheAnimatorId.Instance ().DidFix, true);
		}
	}

	public bool IsTheLastLoop()
	{
		return (MinCountToPass - passCount) == 1;
	}

	public void StartScrolling()
	{
		animator.SetTrigger (TheAnimatorId.Instance ().StartScrolling);
	}

	public void Setup(int first, int final)
	{
		currentDigit = first;
		theFinalDigit = final;
	}

	void Start()
	{
		animator = GetComponent<Animator> ();
		Debug.Assert (animator);
		Debug.Assert (Test.Util.HasAnimatorParameter (animator, TheAnimatorId.Instance (true).DidFix));
		Debug.Assert (Test.Util.HasAnimatorParameter (animator, TheAnimatorId.Instance ().StartScrolling));

		text = GetComponent<Text> ();
		Debug.Assert (text);

		tryFix = false;
	}

}

}
