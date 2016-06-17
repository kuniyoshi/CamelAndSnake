﻿using System.Collections;
using UnityEngine;

namespace Play
{

public class GameController : MonoBehaviour
{

	public GameObject word;
	public ParticleSystem clockPointer;

	WordAnimation wordAnimation;

	void Start()
	{
		Debug.Assert (word);
		wordAnimation = word.GetComponent<WordAnimation> ();
		Debug.Assert (wordAnimation);

		Debug.Assert (clockPointer);
		clockPointer.Simulate (clockPointer.duration);
	}

	void Update()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			Vector3 mousePosition = Input.mousePosition;
			mousePosition.z = 20f - Camera.main.transform.position.z;
			Vector3 worldPosition = Camera.main.ScreenToWorldPoint (mousePosition);
			Debug.Log (worldPosition);

			clockPointer.transform.position = worldPosition;
			Debug.Log ("time before play: " + clockPointer.time);
			clockPointer.Play ();
		}

		if (Input.GetButtonDown("Fire2"))
		{
//			Debug.Log ("fire2");
//			clockPointer.Pause ();
//			Debug.Log ("time: " + clockPointer.time);
//			Debug.Log ("time: " + clockPointer.time);
			clockPointer.Simulate (clockPointer.duration);
//			clockPointer.Stop ();
		}
	}

	Vector3 SpecifyWorldPoint()
	{
		Vector3 mousePosition = Input.mousePosition;
		mousePosition.z = word.GetZ ();
	}

}

}
