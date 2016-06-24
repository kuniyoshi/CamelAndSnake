using UnityEngine;

namespace Title
{

public class GameController : MonoBehaviour
{

	enum State
	{
		Disabled,
		Starting,
		Loading,
		Finished,
	}

	public TouchToStart touchToStart;
	public TouchInterface touchInterface;

	State currentState = State.Disabled;

	void Awake()
	{
		TheAnimatorId.Create();
//		TheScene.Create ();
	}

	void Start()
	{
		Debug.Assert (touchToStart != null);
		Debug.Assert (touchInterface != null);
		touchInterface.Subscribe (delegate(object o, TouchCompleteArg arg) {
			currentState = State.Starting;
		});
	}

	void Update()
	{

		switch (currentState)
		{
		case State.Disabled:
			break;
		case State.Starting:
			currentState = State.Loading;
			touchToStart.InflateOnce ();
			touchToStart.Subscribe (delegate() {
				currentState = State.Finished;
			});
			break;
		case State.Finished:
			UnityEngine.SceneManagement.SceneManager.LoadScene ("Setting");
			break;
		}

	}

}

}
