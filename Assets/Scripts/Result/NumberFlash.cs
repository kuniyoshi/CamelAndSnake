using System.Collections;
using UnityEngine;

namespace Result
{

public class NumberFlash : MonoBehaviour
{

	static float MagicScaleFactor = 0.1f;
	static float MagicTuneFactor = 1.8f;

	public GameObject prefab;

	TextMesh mesh;
//	CharacterAnimation chars;
	GameObject[] objects;
	bool didSetup;

	void Awake()
	{
		Debug.Assert (prefab);
	}

	void Start()
	{
		mesh = GetComponent<TextMesh> ();
		Debug.Assert (mesh);

		objects = new GameObject[mesh.text.Length];
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
		string backup = mesh.text;
		float width = 0f;

		mesh.text = "0123456789";
		CharacterInfo info;

		for (int i = 0; i < mesh.text.Length; i++)
		{
			mesh.font.GetCharacterInfo (mesh.text [i], out info, mesh.font.fontSize, mesh.fontStyle);

			float candidate = (float)info.advance + (float)info.bearing;

			if (candidate > width)
			{
				width = candidate;
			}
		}

		mesh.text = backup;

		return width;
	}

	void InitChildren(GameObject[] objects)
	{
		float width = GetWidth () * mesh.font.fontSize * MagicScaleFactor;
		Debug.Log ("width: " + width);
		float offset = 0f;
		Vector3 pos = transform.position;
		Vector3 one = Vector3.one;
		Vector3 zero = Vector3.zero;

		Debug.Log ("p local scale: " + transform.localScale);

		for (int i = 0; i < objects.Length; i++)
		{
			GameObject anObject = objects [i];
			anObject.transform.SetParent (transform);
			Debug.Log ("c local scale: " + anObject.transform.localScale);
			Debug.Log ("c local position: " + anObject.transform.localPosition);
			Debug.Log ("c position: " + anObject.transform.position);
//			anObject.transform.localScale = one;
//			anObject.transform.localPosition = zero;
			anObject.name = "digit." + i.ToString ();

//			Debug.Log ("p pos: " + transform.position);
//			Debug.Log ("p local pos: " + transform.localPosition);
//			pos = transform.position;
//			pos.x = pos.x + offset;
//			anObject.transform.position = pos;
//			Debug.Log (pos);
//			anObject.transform.localPosition = pos;
//			anObject.transform.position = pos;

//			Debug.Log ("c pos: " + anObject.transform.position);
//			Debug.Log ("c local pos: " + anObject.transform.localPosition);

			offset = offset + width;// * MagicTuneFactor;
		}
	}

}

}
