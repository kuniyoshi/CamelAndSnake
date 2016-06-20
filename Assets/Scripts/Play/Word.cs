using System.Collections;
using UnityEngine;

namespace Play
{

public class Word : MonoBehaviour
{

	static int BufferSize = 20;

	public GameObject prefab;

	WordAnimation bitAnimation;

	public string Text { get { return bitAnimation.Text; } set { bitAnimation.Text = value; } }

	public float Z { get { return bitAnimation.Z; } }

	public void Hide()
	{
		StartCoroutine ("HideDelayed");
	}

	public void Show()
	{
		StartCoroutine ("ShowDelayed");
	}

	public void TextTo(string newText, Vector3 newPoint)
	{
		WarpTo (newPoint);
		Text = newText;
	}

	public void WarpTo(Vector3 point) { bitAnimation.WarpTo(point); }

	void Awake()
	{
		Debug.Assert (prefab);
	}

	void Start()
	{
		GameObject[] objects = new GameObject[BufferSize];

		for (int i = 0; i < BufferSize; i++)
		{
			objects [i] = Instantiate (prefab);
		}

		TextMesh mesh = GetComponent<TextMesh> ();
		Debug.Assert (mesh);

		bitAnimation = new WordAnimation (transform, mesh, objects);
	}

	IEnumerator HideDelayed()
	{
		for (int i = 0; i < bitAnimation.Length; i++)
		{
			bitAnimation.HideAt (i);
			yield return null;
		}
	}

	IEnumerator ShowDelayed()
	{
		for (int i = 0; i < bitAnimation.Length; i++)
		{
			bitAnimation.ShowAt (i);
			yield return null;
		}
	}
}

}
