using System.Collections;
using UnityEngine;

namespace Play
{

public class Progress : MonoBehaviour
{

	public GameObject camelChild;
	public GameObject snakeChild;

	Fraction camel;
	Fraction snake;

	public bool DidCamelComplete() { return camel.DidComplete(); }

	public bool DidComplete() { return camel.DidComplete () && snake.DidComplete (); }

	public bool DidSnakeComplete() { return snake.DidComplete(); }

	public void IncrementCamel()
	{
		camel.Increment ();
	}

	public void IncrementSnake()
	{
		snake.Increment ();
	}

	public void InitCount(int count)
	{
		camel.Init (count);
		snake.Init (count);
	}

	void Awake()
	{
		Debug.Assert(camelChild);
		Debug.Assert(snakeChild);
		camel = camelChild.GetComponent<Fraction> ();
		snake = snakeChild.GetComponent<Fraction> ();
	}

	void Update()
	{}

}

}
