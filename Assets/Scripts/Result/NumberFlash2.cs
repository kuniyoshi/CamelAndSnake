using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Result
{

public class NumberFlash2 : MonoBehaviour
{

	static float MagicScaleFactor = 0.1f;
	static float MagicTuneFactor = 0.1f;

	public GameObject prefab;

	Text text;
	GameObject[] objects;
	bool didSetup;

	void Awake()
	{
		Debug.Assert (prefab);
	}

	void Start()
	{
		text = GetComponent<Text> ();
		Debug.Assert (text);

		objects = new GameObject[text.text.Length];
//		GameObject[] objects = new GameObject[mesh.text.Length];

		for (int i = 0; i < objects.Length; i++)
		{
			objects [i] = Instantiate (prefab);
		}

//		chars = new CharacterAnimation (transform, mesh, objects);
		InitChildren (objects);
	}

	void Update()
	{
	}

	float GetWidth()
	{
		string backup = text.text;
		float width = 0f;

		text.text = "0123456789";
		CharacterInfo info;

		for (int i = 0; i < text.text.Length; i++)
		{
			text.font.GetCharacterInfo (text.text [i], out info, text.font.fontSize, text.fontStyle);

			float candidate = (float)info.advance + (float)info.bearing;

			if (candidate > width)
			{
				width = candidate;
			}
		}

		text.text = backup;

		return width;
	}

	void InitChildren(GameObject[] objects)
	{
		float width = GetWidth () * text.font.fontSize * MagicScaleFactor;
		float offset = -width * (float)objects.Length * (0.5f + MagicTuneFactor);
		Vector3 pos;

		for (int i = 0; i < objects.Length; i++)
		{
			GameObject anObject = objects [i];
			anObject.transform.SetParent (transform, false);
			anObject.name = "digit." + i.ToString ();
			pos = anObject.transform.localPosition;
			pos.x = pos.x + offset;
			anObject.transform.localPosition = pos;
			offset = offset + 1.5f * width; // care char is centered
		}
	}

}

}
