using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Result
{

public class GameController : MonoBehaviour
{

	public NumberFlash score;

	void Awake()
	{
		Debug.Assert (score);
		TheAnimatorId.Create ();
	}

	void Start()
	{
	}

	void Update()
	{
		if (Input.GetButtonDown("Fire1"))
		{
		}

		if (Input.GetButtonDown("Fire2"))
		{
		}
	}

	void OnDestroy()
	{
		TheAnimatorId.Destroy ();
	}

}

}
