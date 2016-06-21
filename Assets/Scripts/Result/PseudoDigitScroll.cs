using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Result
{

public class PseudoDigitScroll : MonoBehaviour
{

	public float yPosition;

	Animator animator;
//	TextMesh textMesh;
	Text numberText;
	int number;
	float firstY;
	float minY;
	float maxY;
	RectTransform rt;

	public void Increment()
	{
		++number;
		number = number % 10;
		numberText.text = number.ToString ();
//		textMesh.text = number.ToString ();
	}

	void Start()
	{
		animator = GetComponent<Animator> ();
		Debug.Assert (animator);
//		Debug.Assert (Test.Util.HasAnimatorParameter (animator, TheAnimatorId.Instance ().ShrinikUp));
//		Debug.Assert (Test.Util.HasAnimatorParameter (animator, TheAnimatorId.Instance ().ShrinikDown));
//		Debug.Assert (animator.HasState (0, TheAnimatorId.Instance ().Normal));
//		textMesh = GetComponent<TextMesh> ();
//		Debug.Assert (textMesh);

		numberText = GetComponent<Text> ();
		Debug.Assert (numberText);

		rt = GetComponent<RectTransform> ();
		Debug.Assert (rt);

		number = 0;
//		firstY = transform.position.y;
		firstY = rt.position.y;
		Debug.Log ("first y: " + firstY);

		minY = 10f;
		maxY = 10f;
	}

	void Update()
	{
//		Vector3 pos = transform.position;
		Vector3 pos = rt.position;
		float rate = Mathf.Clamp01 (yPosition * 0.5f + 0.5f);
		pos.y = Mathf.Lerp (firstY - minY, firstY + maxY, rate);
//		rt.position = pos;
	}

}

}
