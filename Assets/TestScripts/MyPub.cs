using UnityEngine;
using UnityEngine.UI;
using TestScripts;

namespace TestScripts
{

public class MyPub : MonoBehaviour
{

	static OnFinish onFinish;
	static System.EventArgs arg = new System.EventArgs ();

	static public void Subscribe(OnFinish newScriber)
	{
		onFinish += newScriber;
	}

	Text text;
	Color color;
	
	void Start()
	{
		text = GetComponent<Text> ();
		color = text.color;
	}

	void Update()
	{
		if (Input.GetButton("Fire1"))
		{
			if (onFinish != null)
			{
				onFinish (this, arg);
				onFinish = null;
			}

			text.color = new Color (1f, 0f, 0f);
		}
		else
		{
			text.color = color;
		}
	}

}

}
