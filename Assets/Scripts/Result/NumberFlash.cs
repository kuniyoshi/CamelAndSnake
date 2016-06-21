using System.Collections;
using UnityEngine;

public class NumberFlash : CharacterAnimation
{

	int theScrollTrigger;
	int theFixTrigger;
	int theNumber;

	public NumberFlash(Transform transform, TextMesh mesh, GameObject[] objects)
	: base(transform, mesh, objects)
	{}

	public void IncrementNumber()
	{
		
	}

	public int Number { get { return theNumber; } set { theNumber = value; } }

	public void StartScrolling(int to)
	{
//		SetTriggerTo (to);
	}

}
