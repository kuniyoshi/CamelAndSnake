using System.Collections;
using UnityEngine;

namespace Result
{

public class ClimbingUp : MonoBehaviour
{

	public float rate;

	bool isAnimating;

	void StartAnimation()
	{
		isAnimating = true;
	}

	void StopAnimation()
	{
		isAnimating = false;
	}

	void Start()
	{
		isAnimating = false;
	}

	void Update()
	{
		if (!isAnimating)
		{
			return;
		}
	}

}

}
