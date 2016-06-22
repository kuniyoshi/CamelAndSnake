﻿using System.Collections;
using UnityEngine;

namespace Result
{

public class DigitScroll : MonoBehaviour
{

	static int MinCountToPass = 2;

	int currentDigit;
	int lastDigit;
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

		if (tryFix && currentDigit == lastDigit)
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

	public void Setup(int first, int last)
	{
		currentDigit = first;
		lastDigit = last;
	}

	void Start()
	{
		animator = GetComponent<Animator> ();
		Debug.Assert (animator);
		Debug.Assert (Test.Util.HasAnimatorParameter (animator, TheAnimatorId.Instance (true).DidFix));

		mesh = GetComponent<TextMesh> ();
		Debug.Assert (mesh);

		tryFix = false;
	}

}

}
