using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Result
{

public class GameController : MonoBehaviour
{

	public NumberFlash score;
	public WhoWon whoWon;

	void Awake()
	{
		Debug.Assert (score);
		Debug.Assert (whoWon);
		TheAnimatorId.Create ();
	}

	void Start()
	{
	}

	void Update()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			score.SetScore (12345);
			score.StartScrolling ();
		}

		if (Input.GetButtonDown("Fire2"))
		{
			score.StopScrolling ();
		}

		if (Input.GetButtonDown("Fire3"))
		{
			whoWon.Show ();
		}
	}

	void OnDestroy()
	{
		TheAnimatorId.Destroy ();
	}

}

}
