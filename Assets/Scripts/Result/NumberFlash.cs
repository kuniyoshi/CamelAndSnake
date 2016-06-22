using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Result
{

public class NumberFlash : MonoBehaviour
{

	static float MagicScaleFactor = 0.1f;
	static float MagicTuneFactor = 0.1f;

	public GameObject prefab;
	public float MaxDelayedSeconds = 1f;

	Text text;
	GameObject[] objects;
	DigitScroll[] children;

	public bool DidComplete()
	{
		for (int i = 0; i < children.Length; i++)
		{
			if (!children [i].DidComplete ())
			{
				return false;
			}
		}

		return true;
	}

	public void SetScore(int score)
	{
		string scoreText = score.ToString ();
		Debug.Log ("score text len: " + scoreText.Length);
		Debug.Log ("child len: " + children.Length);
		Debug.Assert (scoreText.Length <= children.Length);

		for (int i = 0; i < scoreText.Length; i++)
		{
			int childIndex = children.Length - 1 - i;
			int digit;
			bool didSucceed = int.TryParse (scoreText [i].ToString (), out digit);
			Debug.Assert (didSucceed);
			children [childIndex].Setup (Random.Range(0, 9), digit);
		}

		for (int i = scoreText.Length; i < children.Length; i++)
		{
			children [i].Setup (0, 0);
		}
	}

	public void StartScrolling()
	{
		StartCoroutine ("StartDelayedAnimation");
	}

	public void StopScrolling()
	{
		for (int i = 0; i < children.Length; i++)
		{
			children [i].Fix ();
		}
	}

	void Awake()
	{
		Debug.Assert (prefab);
	}

	void Start()
	{
		text = GetComponent<Text> ();
		Debug.Assert (text);

		objects = new GameObject[text.text.Length];

		for (int i = 0; i < objects.Length; i++)
		{
			objects [i] = Instantiate (prefab);
		}

		InitChildren (objects);

		children = new DigitScroll[objects.Length];

		for (int i = 0; i < objects.Length; i++)
		{
			int index = objects.Length - 1 - i;
			children [i] = objects [index].GetComponentInChildren<DigitScroll> ();
			Debug.Assert (children [i]);
		}

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

	IEnumerator StartDelayedAnimation()
	{
		for (int i = 0; i < children.Length; i++)
		{
			children [i].StartScrolling ();
			yield return new WaitForSeconds (Random.value * MaxDelayedSeconds);
		}
	}

}

}
