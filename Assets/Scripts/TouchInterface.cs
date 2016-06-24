using UnityEngine;

public class TouchInterface : MonoBehaviour
{

	public static float TouchShouldUpInDistance = 0.03f;

	enum State
	{
		Floating,
		TouchingBegin,
		Touching,
		Touched,
	}

	public ParticleSystem touchCircle;

	float sqrTouchShouldUpInDistance;
	State currentState = State.Floating;
	int currentId;
	Vector3 firstPoint;
	Vector3 lastPoint;
	OnTouchEvent onTouchBegin;
	OnTouchEvent onTouchComplete;

	public bool EnablePlayOnComplete { get; set; }

	public float Z { get; set; }

	public void PauseParticle()
	{
		touchCircle.Simulate (0f);
	}

	public void PlayParticleAt(Vector3 position)
	{
		touchCircle.transform.position = position;
		touchCircle.Emit (1);
		touchCircle.Play ();
	}

	public void Subscribe(OnTouchEvent newDelegate)
	{
		onTouchComplete += newDelegate;
	}

	public void SubscribeTouchBegin(OnTouchEvent newDelegate)
	{
		onTouchBegin += newDelegate;
	}

	public void Unsubscribe(OnTouchEvent leaveEvent)
	{
		onTouchComplete -= leaveEvent;
	}

	public void UnsubscribeTouchBegin(OnTouchEvent leaveEvent)
	{
		onTouchBegin -= leaveEvent;
	}

	void Awake()
	{
		Debug.Assert (touchCircle);
		EnablePlayOnComplete = true;
	}

	void Start()
	{
		PauseParticle ();
		sqrTouchShouldUpInDistance = TouchShouldUpInDistance * TouchShouldUpInDistance;
	}

	void Update()
	{
		switch (currentState)
		{
		case State.Floating:
			DoFloating ();
			break;
		case State.TouchingBegin:
			DoTouchingBegin ();
			break;
		case State.Touching:
			DoTouching ();
			break;
		}

		if (currentState == State.Touched)
		{
			currentState = State.Floating;
			Vector3 worldPosition = GetLastWorldPosition ();

			if (EnablePlayOnComplete)
			{
				PlayParticleAt (worldPosition);
			}

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

		currentState = State.TouchingBegin;

		Touch touch = Input.touches [0];
		firstPoint = touch.position;
		currentId = touch.fingerId;
	}

	void DoTouchingBegin()
	{
		Debug.Assert (currentState == State.TouchingBegin);
		currentState = State.Touching;

		if (onTouchBegin != null)
		{
			TouchCompleteArg arg = new TouchCompleteArg (GetFirstWorldPosition (), Time.realtimeSinceStartup);
			onTouchBegin (this, arg);
		}
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
					lastPoint = touches [i].position;
					break;
				}
			}

			didEnd = !didFind;
		}

		if (!didEnd)
		{
			return;
		}

		Vector3 deltaPosition = Camera.main.ScreenToViewportPoint (lastPoint - firstPoint);

		if (deltaPosition.sqrMagnitude < sqrTouchShouldUpInDistance)
		{
			currentState = State.Touched;
		}
		else
		{
			currentState = State.Floating;
		}
	}

	Vector3 GetFirstWorldPosition()
	{
		return GetWorldPoint (firstPoint);
	}

	Vector3 GetLastWorldPosition()
	{
		return GetWorldPoint (lastPoint);
	}

	Vector3 GetWorldPoint(Vector3 point)
	{
		point.z = Z - Camera.main.transform.position.z;
		point = Camera.main.ScreenToWorldPoint (point);
		return point;
	}

}
