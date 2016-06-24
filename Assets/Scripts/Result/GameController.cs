using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Result
{

public class GameController : MonoBehaviour
{

	enum State
	{
		Initial,
		DigitScrollingBegin,
		DigitScrolling,
		DigitStopping,
		Resulting,
		Completed,
	}

	public float ScrollingDuration = 2f;
	public NumberFlash camelScore;
	public NumberFlash snakeScore;
	public WhoWon whoWon;

	Record record;
	State currentState;
	float time;

	void Awake()
	{
		Debug.Assert (camelScore);
		Debug.Assert (snakeScore);
		Debug.Assert (whoWon);
		TheAnimatorId.Create ();
	}

	void Start()
	{
		record = new Record ();
		record.Load ();
		currentState = State.Initial;
	}

	void Update()
	{

		switch (currentState)
		{
		case State.Initial:
			currentState = State.DigitScrolling;
			whoWon.Who = record.CamelScore > record.SnakeScore ? "CAMEL" : "SNAKE";
			camelScore.SetScore (record.CamelScore);
			snakeScore.SetScore (record.SnakeScore);
			camelScore.StartScrolling ();
			snakeScore.StartScrolling ();
			break;
		case State.DigitScrollingBegin:
			currentState = State.DigitScrolling;
			time = Time.time;
			break;
		case State.DigitScrolling:
			if (Time.time - time > ScrollingDuration)
			{
				currentState = State.DigitStopping;
				camelScore.StopScrolling ();
				snakeScore.StopScrolling ();
			}
			break;
		case State.DigitStopping:
			if (camelScore.DidComplete() && snakeScore.DidComplete())
			{
				currentState = State.Resulting;
				whoWon.Show ();
			}
			break;
		case State.Resulting:
			if (whoWon.DidComplete())
			{
				currentState = State.Completed;
			}
			break;
		case State.Completed:
			UnityEngine.SceneManagement.SceneManager.LoadScene ("Settings");
			break;
		}
	}

	void OnDestroy()
	{
		TheAnimatorId.Destroy ();
	}

}

}
