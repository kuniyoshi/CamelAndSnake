using System.Collections;
using UnityEngine;

public class TouchInterface : MonoBehaviour
{

	public float touchShouldUpInDistance = 0.03f;
	float _sqrTouchShouldUpInDistance;

	public ParticleSystem touchCircle;

	enum State
	{
		Floating,
		Touching,
		Touched,
	}

	State currentState = State.Floating;
	int currentId;
	Vector3 startPosition;
	Vector3 lastPosition;


	void Start()
	{
		_sqrTouchShouldUpInDistance = touchShouldUpInDistance * touchShouldUpInDistance;
		Debug.Assert (touchCircle);
//		touchCircle.Pause ();
//		touchCircle.Clear ();
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
			Debug.Log ("world: " + worldPosition);
			touchCircle.transform.position = worldPosition;
//			touchCircle.time = 0f;
			touchCircle.Emit (1);
			touchCircle.Play ();
			Debug.Log ("particle system pos: " + touchCircle.transform.position);
		}
	}

	void DoFloating()
	{
		Debug.Assert (currentState == State.Floating);

//		touchCircle.Pause ();
//		touchCircle.Clear ();
		
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
		Debug.Log ("last position: " + lastPosition);
		Debug.Log ("start position: " + startPosition);
		Debug.Log ("magnitude: " + deltaPosition.magnitude);

		if (deltaPosition.sqrMagnitude < _sqrTouchShouldUpInDistance)
		{
			currentState = State.Touched;
		}
		else
		{
			currentState = State.Floating;
		}
	}

}
