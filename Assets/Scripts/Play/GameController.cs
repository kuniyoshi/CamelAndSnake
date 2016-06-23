using System.Collections;
using UnityEngine;

namespace Play
{

public class GameController : MonoBehaviour
{

	enum State
	{
		Setting,
		Floating,
		Preparing,
		Recognizing,
		Scoring,
		NotifyingBegin,
		Notifying,
		Completed,
	}

	enum CharType
	{
		Undefined,
		Camel,
		Snake,
	}

	static float MagicTuneValue = 7.5f;

	public SceneStrider configHolder;
	public ParticleSystem clockPointer;
	public Word phrase;
	public Word recognitionTime;
	public Progress progress;
	public Complete complete;

	State currentState;
	Configuration config;
	Vocabulary vocabulary;
	CharType currentChar;
	float score;
	Record record;

	void Awake()
	{
		TheAnimatorId.Create ();
		Random.seed = (int)(Time.realtimeSinceStartup * 1000f);

		Debug.Assert (configHolder);
		Debug.Assert (clockPointer);
		Debug.Assert (phrase);
		Debug.Assert (recognitionTime);
		Debug.Assert (progress);
		Debug.Assert (complete);
	}

	void Start()
	{
		currentState = State.Setting;
		clockPointer.Simulate (clockPointer.duration);
		vocabulary = new Vocabulary ();
		currentChar = CharType.Undefined;
		record = new Record ();
	}

	void Update()
	{

		switch (currentState)
		{
		case State.Setting:
			UpdateSetting ();
			break;
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
		case State.NotifyingBegin:
			UpdateNotifyingBegin ();
			break;
		case State.Notifying:
			UpdateNotifying ();
			break;
		case State.Completed:
			UpdateCompleted ();
			break;
		}

	}

	void OnDestroy()
	{
		TheAnimatorId.Destroy ();
	}

	CharType GetNextChar()
	{
		CharType nextChar = CharType.Undefined;

		switch (config.order)
		{
		case Configuration.LetterCase.Camel:
			nextChar = CharType.Camel;
			break;
		case Configuration.LetterCase.Snake:
			nextChar = CharType.Snake;
			break;
		case Configuration.LetterCase.Shuffle:
			if (progress.DidComplete())
			{
				; // do nothing
			}
			else if (progress.DidCamelComplete())
			{
				nextChar = CharType.Snake;
			}
			else if (progress.DidSnakeComplete())
			{
				nextChar = CharType.Camel;
			}
			else
			{
				int randValue = Random.Range (0, 2);
				nextChar = randValue == 0 ? CharType.Camel : CharType.Snake;
			}
			break;
		}

		Debug.Assert (nextChar != CharType.Undefined);
		return nextChar;
	}

	string GetNextPhrase()
	{
		if (currentChar == CharType.Snake)
		{
			return vocabulary.NextSnake ();
		}

		Debug.Assert (currentChar == CharType.Camel);

		if (config.camelType == Configuration.CamelType.Camel)
		{
			return vocabulary.NextCamel ();
		}
		if (config.camelType == Configuration.CamelType.Pascal)
		{
			return vocabulary.NextPascal ();
		}
		else
		{
			int which = Random.Range (0, 2);
			return which == 0 ? vocabulary.NextCamel () : vocabulary.NextPascal ();
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

	void UpdateSetting()
	{
		currentState = State.Floating;
		config = configHolder.Configuration;

		if (!config.DidConfigure())
		{
			Debug.Log ("Could not stride configuration!  Use default value");
			SetDefaultConfiguration ();
		}

		switch (config.order)
		{
		case Configuration.LetterCase.Camel:
			progress.InitCamel (config.times);
			progress.InitSnake (0);
			break;
		case Configuration.LetterCase.Snake:
			progress.InitCamel (0);
			progress.InitSnake (config.times);
			break;
		case Configuration.LetterCase.Shuffle:
			progress.InitCamel (config.times);
			progress.InitSnake (config.times);
			break;
		}

		currentChar = GetNextChar ();
	}

	void UpdateFloating()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			currentState = State.Preparing;

			Vector3 point = SpecifyWorldPoint ();

			clockPointer.transform.position = point;
			clockPointer.Play ();

			point.x = point.x + MagicTuneValue;

			currentChar = GetNextChar ();
			phrase.TextTo (GetNextPhrase (), point);
			ScoreThePhraseExpectTime ();
		}
	}

	void UpdatePreparing()
	{
		currentState = State.Recognizing;
		phrase.Show ();
		score = Time.time;
	}

	void UpdateRecognizing()
	{
		if (Input.GetButtonDown("Fire2"))
		{
			currentState = State.Scoring;
			score = Time.time - score;
			Vector3 point = phrase.transform.position;
			point.z = phrase.Z;
			recognitionTime.TextTo (score.ToString (), point);

			IncrementProgress ();

			clockPointer.Pause ();
			clockPointer.Simulate (clockPointer.duration);
		}
	}

	void UpdateScoring()
	{
		currentState = progress.DidComplete() ? State.NotifyingBegin : State.Floating;
		phrase.Hide ();
		recognitionTime.Show ();
		ScoreThePhraseOnlyTime ();
		currentChar = progress.DidComplete () ? CharType.Undefined : GetNextChar ();
	}

	void UpdateNotifyingBegin()
	{
		currentState = State.Notifying;
		complete.Animate ();
	}

	void UpdateNotifying()
	{
		if (complete.DidComplete())
		{
			currentState = State.Completed;
		}
	}

	void UpdateCompleted()
	{
		Debug.Log (record);
		record.Save ();
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Result");
	}

	void ScoreThePhraseExpectTime()
	{
		switch (currentChar)
		{
		case CharType.Camel:
			record.IncrementCamel (vocabulary.CountOfCurrentWords, vocabulary.CountOfCurrentLetters);
			break;
		case CharType.Snake:
			record.IncrementSnake (vocabulary.CountOfCurrentWords, vocabulary.CountOfCurrentLetters);
			break;
		}
	}

	void ScoreThePhraseOnlyTime()
	{
		switch (currentChar)
		{
		case CharType.Camel:
			record.CamelTotalTime = record.CamelTotalTime + score;
			break;
		case CharType.Snake:
			record.SnakeTotalTime = record.SnakeTotalTime + score;
			break;
		}
	}

	void SetDefaultConfiguration()
	{
		Debug.LogAssertion ("This Method is for Debug");
		config.camelType = Configuration.CamelType.Mix;
		config.order = Configuration.LetterCase.Shuffle;
		config.times = 1;
	}

	Vector3 SpecifyWorldPoint()
	{
		Vector3 mousePosition = Input.mousePosition;
		mousePosition.z = phrase.Z - Camera.main.transform.position.z;
		Vector3 worldPosition = Camera.main.ScreenToWorldPoint (mousePosition);
		return worldPosition;
	}

}

}
