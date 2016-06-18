using System.Collections;
using UnityEngine;

namespace Play
{

public class GameController : MonoBehaviour
{

	public ParticleSystem clockPointer;
	public GameObject wordObject;
	public GameObject scoreObject;

	WordAnimation wordAnimation;
	RecognitionTime recognitionTime;

	void Awake()
	{
		Debug.Assert (clockPointer);
		clockPointer.Simulate (clockPointer.duration);
		Debug.Assert (wordObject);
		wordAnimation = wordObject.GetComponent<WordAnimation> ();
		Debug.Assert (wordAnimation);
		Debug.Assert (scoreObject);
		recognitionTime = scoreObject.GetComponent<RecognitionTime> ();
		Debug.Assert (recognitionTime);
	}

	void Start()
	{
		wordAnimation.SetupBuffer (20);
	}

	void Update()
	{

		if (Input.GetButtonDown("Fire1"))
		{
			Vector3 point = SpecifyWorldPoint ();
			recognitionTime.WarpTo (point);
			recognitionTime.Text = "012.3456789";
			recognitionTime.Show ();
		}

		if (Input.GetButtonDown("Fire2"))
		{
			recognitionTime.Hide ();
		}

//		if (Input.GetButtonDown("Fire1"))
//		{
//			Vector3 point = SpecifyWorldPoint ();
//			wordAnimation.WarpTo (point);
//			wordAnimation.Word = "helloWorld";
//			wordAnimation.Show ();
//			recognizingTime = Time.time;
//		}
//
//		if (Input.GetButtonDown("Fire2"))
//		{
//			wordAnimation.Hide ();
//			recognizingTime = recognizingTime - Time.time;
//		}
	}

	Vector3 SpecifyWorldPoint()
	{
		Vector3 mousePosition = Input.mousePosition;
		mousePosition.z = wordAnimation.Z - Camera.main.transform.position.z;
		Vector3 worldPosition = Camera.main.ScreenToWorldPoint (mousePosition);
		return worldPosition;
	}

}

}
