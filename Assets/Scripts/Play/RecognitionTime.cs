using System.Collections;
using UnityEngine;

namespace Play
{

public class RecognitionTime : MonoBehaviour
{

	static int BufferSize = 20;

	public GameObject prefab;

	CharacterAnimation word;

	public string Text { get { return word.Text; } set { word.Text = value; } }

	public void Hide() { word.Hide(); }

	public void Show() { word.Show(); }

	public void WarpTo(Vector3 point) { word.WarpTo(point); }

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

		word = new CharacterAnimation (transform, mesh, objects);
	}

}

}
