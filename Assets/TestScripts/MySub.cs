using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TestScripts;

namespace TestScripts
{

public class MySub : MonoBehaviour
{

	enum State
	{
		Initializing,
		Waiting,
		Working,
	}

	State currentState;
	Text text;

	void Start()
	{
		currentState = State.Initializing;
		text = GetComponent<Text> ();
	}

	void Update()
	{
		switch (currentState)
		{
		case State.Initializing:
			MyPub.Subscribe (delegate(object o, System.EventArgs arg) {
				currentState = State.Working;
			});
			break;

		case State.Waiting:
			break;

		case State.Working:
			StartCoroutine ("BlinkText");
			break;
		}
	}

	IEnumerator BlinkText()
	{
		const float KeepBlinkingSecond = 2f;
		float startAt = Time.realtimeSinceStartup;
		Color color = text.color;

		Debug.Log ("start blinking");

		while (true)
		{
			float delta = Time.realtimeSinceStartup - startAt;

			if (delta > KeepBlinkingSecond)
			{
				break;
			}

			color.a = Mathf.Cos (delta * Mathf.PI * KeepBlinkingSecond);

			text.color = color;

			yield return null;
		}

		Debug.Log ("end blink text");
		currentState = State.Initializing;

		yield return null;
	}

}

}
