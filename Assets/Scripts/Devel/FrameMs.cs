using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Devel
{

public class FrameMs : MonoBehaviour
{

	Text text;

	void Start()
	{
		text = GetComponent<Text> ();
		Debug.Assert (text);
	}

	void Update()
	{
		int ms = (int)(Time.deltaTime * 1000 + 0.5);
		text.text = ms.ToString ();
	}

}

}
