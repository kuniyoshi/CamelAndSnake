using System.Collections;
using UnityEngine;

namespace Play
{

public class GameController : MonoBehaviour
{

	public ParticleSystem clockPointer;
	public GameObject wordObject;
	public GameObject scoreObject;

	Word vocabulary;
	Word recognitionTime;

	void Awake()
	{
		Debug.Assert (clockPointer);
		clockPointer.Simulate (clockPointer.duration);
		Debug.Assert (wordObject);
		vocabulary = wordObject.GetComponent<Word> ();
		Debug.Assert (vocabulary);
		Debug.Assert (scoreObject);
		recognitionTime = scoreObject.GetComponent<Word> ();
		Debug.Assert (recognitionTime);
	}

	void Start()
	{
	}

	void Update()
	{

//		if (Input.GetButtonDown("Fire1"))
//		{
//			Vector3 point = SpecifyWorldPoint ();
//			recognitionTime.WarpTo (point);
//			recognitionTime.Text = "012.3456789";
//			recognitionTime.Show ();
//		}
//
//		if (Input.GetButtonDown("Fire2"))
//		{
//			recognitionTime.Hide ();
//		}

		if (Input.GetButtonDown("Fire1"))
		{
			Vector3 point = SpecifyWorldPoint ();
			vocabulary.TextTo ("helloWorld", point);
			vocabulary.Show ();
//			recognizingTime = Time.time;
		}

		if (Input.GetButtonDown("Fire2"))
		{
			vocabulary.Hide ();
//			recognizingTime = recognizingTime - Time.time;
		}

	}

	Vector3 SpecifyWorldPoint()
	{
		Vector3 mousePosition = Input.mousePosition;
		mousePosition.z = vocabulary.Z - Camera.main.transform.position.z;
		Vector3 worldPosition = Camera.main.ScreenToWorldPoint (mousePosition);
		return worldPosition;
	}

}

}
