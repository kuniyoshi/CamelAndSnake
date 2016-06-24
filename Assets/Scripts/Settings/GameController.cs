using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Settings
{

public class GameController : MonoBehaviour
{

	enum State
	{
		Initializing,
		Configuring,
	}

	readonly int[] Times = { 10, 20, 30, 40, 50 };

	public TouchInterface touchInterface;
	public Toggle[] camelizationToggles;
	public Toggle[] orderToggles;
	public StartButton startButton;
	public SceneStrider sceneStrider;

	State currentState;
	Configuration config;

	public void TrackTimes(float sliderValue)
	{
		int times = Times [(int)sliderValue];
		config.times = times;
	}

	void Awake()
	{
		Debug.Assert (touchInterface);

		for (int i = 0; i < camelizationToggles.Length; i++)
		{
			Debug.Assert (camelizationToggles [i]);
		}

		for (int i = 0; i < orderToggles.Length; i++)
		{
			Debug.Assert (orderToggles [i]);
		}

		Debug.Assert (startButton);
		Debug.Assert (sceneStrider);
	}

	void Start()
	{
		currentState = State.Initializing;
		config.times = Times [0];
	}

	void Update()
	{
		switch (currentState)
		{
		case State.Initializing:
			currentState = State.Configuring;

			touchInterface.EnablePlayOnComplete = true;
			touchInterface.Z = 0f;

			for (int i = 0; i < camelizationToggles.Length; i++)
			{
				TrackCamelType (camelizationToggles [i]);
			}

			for (int i = 0; i < orderToggles.Length; i++)
			{
				TrackLetterCase (orderToggles [i]);
			}

			startButton.Subscribe (delegate() {
				sceneStrider.StartStriding(config);
				UnityEngine.SceneManagement.SceneManager.LoadScene("Play");
			});

			break;
		case State.Configuring:
			break;
		}
	}

	void TrackCamelType(Toggle toggle)
	{
		MyToggle myToggle = toggle.GetComponent<MyToggle> ();

		if (toggle.isOn)
		{
			myToggle.GetConfiguration (out config.camelType);
		}

		myToggle.Subscribe (delegate(object o) {
			if (toggle.isOn)
			{
				((MyToggle)o).GetConfiguration (out config.camelType);
			}
		});
	}

	void TrackLetterCase(Toggle toggle)
	{
		MyToggle myToggle = toggle.GetComponent<MyToggle> ();

		if (toggle.isOn)
		{
			myToggle.GetConfiguration (out config.order);
		}

		myToggle.Subscribe (delegate(object o) {
			if (toggle.isOn)
			{
				((MyToggle)o).GetConfiguration (out config.order);
			}
		});
	}

}

}
