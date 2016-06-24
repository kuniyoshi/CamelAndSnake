using UnityEngine;

public class TouchInterface : MonoBehaviour
{

	public static float TouchShouldUpInDistance = 0.03f;

	enum State
	{
		Floating,
		Touching,
		Touched,
	}

	public ParticleSystem touchCircle;

	float sqrTouchShouldUpInDistance;
	State currentState = State.Floating;
	int currentId;
	Vector3 startPosition;
	Vector3 lastPosition;
	OnTouchComplete onTouchComplete;

	public void Subscribe(OnTouchComplete newDelegate)
	{
		onTouchComplete += newDelegate;
	}

	void Awake()
	{
		Debug.Assert (touchCircle);
	}

	void Start()
	{
		touchCircle.Pause ();
		sqrTouchShouldUpInDistance = TouchShouldUpInDistance * TouchShouldUpInDistance;
	}

	void Update()
	{
		switch (currentState)
		{
		case State.Floating:
			DoFloating ();
			break;
		case State.Touching:
			DoTouching ();
			break;
		}

		if (currentState == State.Touched)
		{
			currentState = State.Floating;
			Vector3 worldPosition = lastPosition;
			worldPosition.z = -Camera.main.transform.position.z;
			worldPosition = Camera.main.ScreenToWorldPoint (worldPosition);
			touchCircle.transform.position = worldPosition;
			touchCircle.time = 0f;
			touchCircle.Play ();

			if (onTouchComplete != null)
			{
				TouchCompleteArg arg = new TouchCompleteArg (worldPosition, Time.realtimeSinceStartup);
				onTouchComplete (this, arg);
			}
		}
	}

	void DoFloating()
	{
		Debug.Assert (currentState == State.Floating);

		if (Input.touchCount == 0)
		{
			return;
		}

		Touch touch = Input.touches [0];
		startPosition = touch.position;
		currentId = touch.fingerId;

		currentState = State.Touching;
	}

	void DoTouching()
	{
		Debug.Assert (currentState == State.Touching);
		bool didEnd = Input.touchCount == 0;

		if (!didEnd)
		{
			Touch[] touches = Input.touches;
			bool didFind = false;

			for (int i = 0; i < touches.Length; i++)
			{
				if (touches[i].fingerId == currentId)
				{
					didFind = true;
					lastPosition = touches [i].position;
					break;
				}
			}

			didEnd = !didFind;
		}

		if (!didEnd)
		{
			return;
		}

		Vector3 deltaPosition = Camera.main.ScreenToViewportPoint (lastPosition - startPosition);

		if (deltaPosition.sqrMagnitude < sqrTouchShouldUpInDistance)
		{
			currentState = State.Touched;
		}
		else
		{
			currentState = State.Floating;
		}
	}

}
