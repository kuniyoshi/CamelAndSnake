using System.Collections;
using UnityEngine;

namespace Play
{

public class Word : MonoBehaviour
{

	static int BufferSize = 20;

	public GameObject prefab;

	CharacterAnimation animation;

	public string Text { get { return animation.Text; } set { animation.Text = value; } }

	public float Z { get { return animation.Z; } }

	public void Hide() { animation.Hide(); }

	public void Show() { animation.Show(); }

	public void TextTo(string newValue, Vector3 newPoint)
	{
		WarpTo (newPoint);
		Text = newValue;
	}

	public void WarpTo(Vector3 point) { animation.WarpTo(point); }

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

		animation = new CharacterAnimation (transform, mesh, objects);
	}

}

}
