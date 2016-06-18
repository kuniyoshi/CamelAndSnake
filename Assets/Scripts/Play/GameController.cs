using System.Collections;
using UnityEngine;

namespace Play
{

public class GameController : MonoBehaviour
{

	enum State
	{
		Floating,
		Preparing,
		Recognizing,
		Scoring,
		Notifying,
	}

	enum CharType
	{
		Undefined,
		Camel,
		Snake,
	}

	public ParticleSystem clockPointer;
	public GameObject wordObject;
	public GameObject scoreObject;
	public Progress progress;
	public Complete complete;

	Word vocabulary;
	Word recognitionTime;
	State currentState;
	CharType currentChar;
	float score;

	void Awake()
	{
		Debug.Assert (clockPointer);
		clockPointer.Simulate (clockPointer.duration);
		Debug.Assert (wordObject);
		vocabulary = wordObject.GetComponent<Word> ();
		Debug.Assert (vocabulary);
		Debug.Assert (scoreObject);
		recognitionTime = scoreObject.GetComponent<Word> ();
		Debug.Assert (recognitionTime);
		Debug.Assert (progress);
		Debug.Assert (complete);
	}

	void Start()
	{
		currentState = State.Floating;
		progress.InitCamel (2);
		progress.InitSnake (0);
		currentChar = CharType.Undefined;
	}

	void Update()
	{

		switch (currentState)
		{
		case State.Floating:
			UpdateFloating ();
			break;
		case State.Preparing:
			UpdatePreparing ();
			break;
		case State.Recognizing:
			UpdateRecognizing ();
			break;
		case State.Scoring:
			UpdateScoring ();
			break;
		case State.Notifying:
			UpdateNotifying ();
			break;
		}

	}

	void IncrementProgress()
	{
		Debug.Assert (currentChar != CharType.Undefined);

		switch (currentChar)
		{
		case CharType.Camel:
			progress.IncrementCamel ();
			break;
		case CharType.Snake:
			progress.IncrementSnake ();
			break;
		}
	}

	void UpdateFloating()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			Vector3 point = SpecifyWorldPoint ();
			vocabulary.TextTo ("helloWorld", point);
			currentState = State.Preparing;

			currentChar = CharType.Camel;
		}
	}

	void UpdatePreparing()
	{
		vocabulary.Show ();
		score = Time.time;
		currentState = State.Recognizing;
	}

	void UpdateRecognizing()
	{
		if (Input.GetButtonDown("Fire2"))
		{
			score = Time.time - score;
			Vector3 point = vocabulary.transform.position;
			point.z = vocabulary.Z;
			recognitionTime.TextTo (score.ToString (), point);
			currentState = State.Scoring;

			IncrementProgress ();
			currentChar = CharType.Undefined;
		}
	}

	void UpdateScoring()
	{
		vocabulary.Hide ();
		recognitionTime.Show ();
		currentState = State.Floating;

		if (progress.DidComplete())
		{
			currentState = State.Notifying;
		}
	}

	void UpdateNotifying()
	{
		// finish
		complete.Animate ();

		if (complete.DidComplete())
		{
//			Debug.Log ("FINISH");
		}
	}

	Vector3 SpecifyWorldPoint()
	{
		Vector3 mousePosition = Input.mousePosition;
		mousePosition.z = vocabulary.Z - Camera.main.transform.position.z;
		Vector3 worldPosition = Camera.main.ScreenToWorldPoint (mousePosition);
		return worldPosition;
	}

}

}
