using System.Collections;
using UnityEngine;

namespace Play
{

public class Test : MonoBehaviour
{

	RectTransform rect;

	void Start()
	{
		TextMesh mesh = GetComponent <TextMesh> ();
		string meshText = mesh.text;

		Debug.Log ("font size: " + mesh.fontSize);

		foreach (var bit in meshText)
		{
			CharacterInfo info;
			mesh.font.GetCharacterInfo (bit, out info, mesh.fontSize, mesh.fontStyle);
			Debug.Log (bit + ", height: " + info.glyphHeight);
			Debug.Log ("x: max - min" + (info.maxX - info.minX));
			Debug.Log (bit + ", width: " + info.glyphHeight);
			Debug.Log ("y: max - min" + (info.maxY - info.minY));
			Debug.Log ("advance: " + info.advance);
		}
//		Renderer renderer = mesh.GetComponent<Renderer>();
//		Bounds bounds = renderer.bounds;
//		Debug.Log ("bounds: " + bounds);
//		Debug.Log ("size.x: " + bounds.size.x);
//		Debug.Log ("size.y: " + bounds.size.y);

		rect = GetComponent<RectTransform> ();
	}

	void Update()
	{
		if (Input.GetButtonDown("Fire1"))
		{
				Debug.Log (rect.rect.width);
				Debug.Log (rect.rect.height);
		}
	}

}

}
