using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Result
{

public class GameController : MonoBehaviour
{

	public DigitScroll digitScroll;

	void OnEnabled()
	{
		TheAnimatorId.Create ();
	}

	void Awake()
	{
		Debug.Assert (digitScroll);
		TheAnimatorId.Create ();
	}

	void Start()
	{
		digitScroll.InitialDigit = Random.Range (0, 9);
	}

	void Update()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			Debug.Log ("fire1");
			digitScroll.Fix ();
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
