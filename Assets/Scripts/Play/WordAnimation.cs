using System.Collections;
using UnityEngine;

namespace Play
{

public class WordAnimation : MonoBehaviour
{

	public string word;
	public GameObject prefab;
	public float duration = 3f;

	float[] destinations;
	Transform[] children;
	float startedAt = 0f;
	Vector3[] startPositions;
	Animator[] animators;

	bool didStart;

	void Start()
	{
		destinations = new float[word.Length];
		children = new Transform[word.Length];
		startPositions = new Vector3[word.Length];
		animators = new Animator[word.Length];
		float width = transform.position.x;

		TextMesh goalMesh = GetComponent<TextMesh> ();
		CharacterInfo info;

		for (int i = 0; i < word.Length; i++)
		{
			GameObject bit = Instantiate (prefab);
			bit.transform.parent = transform;
			bit.name = "bit." + i;
			TextMesh mesh = bit.GetComponent<TextMesh> ();
			Debug.Assert (mesh);
			mesh.text = word [i].ToString();

			float x = Random.Range (xRange.x, xRange.y);
			float y = Random.Range (yRange.x, yRange.y);

			bit.transform.position = new Vector3 (x, y, transform.position.z);

			destinations [i] = width;

			goalMesh.font.GetCharacterInfo (word [i], out info, goalMesh.fontSize, goalMesh.fontStyle);
			width += info.advance * 0.1f;

			children [i] = bit.transform;
			startPositions [i] = bit.transform.position;
			animators [i] = bit.GetComponent<Animator> ();
		}

		startedAt = Time.time;

		Vector3 destination = new Vector3 (0f, transform.position.y, transform.position.z);

		for (int i = 0; i < word.Length; i++)
		{
			destination.x = destinations [i];
			children [i].position = destination;
		}
	}

	void Update()
	{
//		Vector3 destination = new Vector3 (0f, transform.position.y, transform.position.z);
//		float currentTime = Mathf.Clamp01 (startedAt + Time.deltaTime);
//		float currentTime = Mathf.Clamp ((Time.time - startedAt) / duration, 0f, 1f);

//		Debug.Log ("" + startedAt + ", " + Time.time + ", " + currentTime);

//		for (int i = 0; i < children.Length; i++)
//		{
//			destination.x = destinations [i];
////			children[i].position = Vector3.Lerp (startPositions[i], destination, currentTime);
//		}

		if (Input.GetButtonDown("Fire1"))
		{
//			StartCoroutine ("ShowCharacter");
		}

		if (Input.GetButtonDown("Fire2"))
		{
//			StartCoroutine ("HideCharacter");
		}

	}

	IEnumerator ShowCharacter()
	{
		for (int i = 0; i < animators.Length; i++)
		{
			animators [i].SetTrigger ("Show");
			yield return null; //new WaitForSeconds(0.05f);
		}
	}

	IEnumerator HideCharacter()
	{
		for (int i = 0; i < animators.Length; i++)
		{
			animators [i].SetTrigger ("Hide");
			yield return null; //new WaitForSeconds(0.05f);
		}
	}

//	IEnumerable HideCharacter()
//	{
//		Debug.Log ("hiding...............");
//
//		for (int i = 0; i < animators.Length; i++)
//		{
//			Debug.Log ("hiding " + i);
//			animators [i].SetTrigger ("Hide");
//			yield return null;
//		}
//	}

}

}
