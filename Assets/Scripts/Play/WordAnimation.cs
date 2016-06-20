using System.Collections;
using UnityEngine;

namespace Play
{

public class WordAnimation : CharacterAnimation
{

	int theHideTrigger;
	int theShowTrigger;

	public WordAnimation(Transform transform, TextMesh mesh, GameObject[] objects)
	: base(transform, mesh, objects)
	{

		theHideTrigger = TheAnimatorId.Instance ().Hide;
		theShowTrigger = TheAnimatorId.Instance ().Show;
		Debug.Assert (TestAnimatorHasTrigger (theHideTrigger));
		Debug.Assert (TestAnimatorHasTrigger (theShowTrigger));
	}

	public void HideAll()
	{
		for (int i = 0; i < Length; i++)
		{
			SetTriggerTo (theHideTrigger, i);
		}
	}

	public void HideAt(int at)
	{
		SetTriggerTo (theHideTrigger, at);
	}

	public void ShowAll()
	{
		for (int i = 0; i < Length; i++)
		{
			SetTriggerTo (TheAnimatorId.Instance ().Show, i);
		}
	}

	public void ShowAt(int at)
	{
		SetTriggerTo (theShowTrigger, at);
	}

}

}
