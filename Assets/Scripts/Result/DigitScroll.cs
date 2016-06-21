using System.Collections;
using UnityEngine;

namespace Result
{

public class DigitScroll : MonoBehaviour
{

	static int MinCountToPass = 2;

	int currentDigit;
	int theLastDigit;
	TextMesh mesh;
	Animator animator;
	bool tryFix;
	int passCount;

	public int InitialDigit { get; set; }

	public void Fix()
	{
		tryFix = true;
		passCount = 0;
	}

	public void IncrementDigit()
	{
		++currentDigit;
		currentDigit = currentDigit % 10;
		mesh.text = currentDigit.ToString ();

		if (tryFix && currentDigit == theLastDigit)
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

	void Awake()
	{
	}

	void Start()
	{
		animator = GetComponent<Animator> ();
		Debug.Assert (animator);
		Debug.Assert (Test.Util.HasAnimatorParameter (animator, TheAnimatorId.Instance (true).DidFix));

		mesh = GetComponent<TextMesh> ();
		Debug.Assert (mesh);

		InitialDigit = 0;
		currentDigit = InitialDigit;

		bool didSucced = int.TryParse (mesh.text, out theLastDigit);
		Debug.Assert (didSucced);

		tryFix = false;
	}

	void Update()
	{
	}
}

}
