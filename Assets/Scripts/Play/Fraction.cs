using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Play
{

public class Fraction : MonoBehaviour
{

	public Text numerator;
	public Text denominator;

	int n;
	int d;

	public bool DidComplete() { return n == d; }

	public void Increment()
	{
		Debug.Assert (n < d);
		++n;
		numerator.text = n.ToString ();
	}

	public void Init(int count)
	{
		d = count;
		n = 0;
		numerator.text = n.ToString ();
		denominator.text = d.ToString ();
	}

	void Awake()
	{
		Debug.Assert (numerator);
		Debug.Assert (denominator);
	}

}

}
