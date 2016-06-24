using UnityEngine;

namespace Title
{

public class GameController : MonoBehaviour
{

	enum LoadingState
	{
		Disabled,
		Starting,
		Loading,
		Finished,
	}

	public TouchToStart touchToStart;
	public TouchInterface touchInterface;

	LoadingState loadingState = LoadingState.Disabled;

	void Awake()
	{
		TheScene.Create ();
	}

	void Start()
	{
		Debug.Assert (touchToStart != null);
		Debug.Assert (touchInterface != null);
		touchInterface.Subscribe (delegate(object o, TouchCompleteArg arg) {
			loadingState = LoadingState.Starting;
		});
	}

	void Update()
	{
		if (loadingState == LoadingState.Disabled)
		{
			return;
		}

		if (loadingState == LoadingState.Starting)
		{
			touchToStart.InflateOnce ();
			touchToStart.Subscribe (delegate() {
				loadingState = LoadingState.Finished;
			});
			loadingState = LoadingState.Loading;
		}

		if (loadingState == LoadingState.Finished)
		{
//			UnityEngine.SceneManagement.SceneManager.LoadScene ("Setting");
			loadingState = LoadingState.Disabled;
		}

	}

}

}
