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
	}

	public ParticleSystem clockPointer;
	public GameObject wordObject;
	public GameObject scoreObject;

	Word vocabulary;
	Word recognitionTime;
	State currentState;
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
	}

	void Start()
	{
		currentState = State.Floating;
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
		}

	}

	void UpdateFloating()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			Vector3 point = SpecifyWorldPoint ();
			vocabulary.TextTo ("helloWorld", point);
			currentState = State.Preparing;
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
		}
	}

	void UpdateScoring()
	{
		vocabulary.Hide ();
		recognitionTime.Show ();
		currentState = State.Floating;
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
