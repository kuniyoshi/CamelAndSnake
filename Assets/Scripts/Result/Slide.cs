using System.Collections;
using UnityEngine;

namespace Result
{

public class Slide : MonoBehaviour
{

	static float Epsilon = 0.001f;

	public float movingRate;

	Canvas canvas;
	Animator animator;
	bool isAnimating;
	float toY;
	float fromY;

	public void SlideOut()
	{
		isAnimating = true;
		canvas.renderMode = RenderMode.WorldSpace;
		RectTransform rect = canvas.GetComponent<RectTransform> ();
		toY = transform.position.y + rect.rect.height * transform.localScale.y + Epsilon;
		animator.SetTrigger ("Up");
		fromY = transform.position.y;
		canvas.sortingOrder = 0;
	}

	public void SlideIn()
	{
		isAnimating = true;
		canvas.renderMode = RenderMode.WorldSpace;
		RectTransform rect = canvas.GetComponent<RectTransform> ();
		Vector3 p = transform.position;
		toY = p.y;
		p.y = p.y - rect.rect.height * transform.localScale.y;
		transform.position = p;
		animator.SetTrigger ("Up");
		fromY = transform.position.y;
		canvas.sortingOrder = 1;
	}

	public void GobackScrenSpaceCamera()
	{
		canvas.renderMode = RenderMode.ScreenSpaceCamera;
	}

	void Start()
	{
		canvas = GetComponent<Canvas> ();
		Debug.Assert (canvas);
		animator = GetComponent<Animator> ();
		Debug.Assert (animator);
	}

	void Update()
	{
		if (!isAnimating)
		{
			return;
		}

		Vector3 p = transform.position;
		p.y = Mathf.Lerp (fromY, toY, movingRate);
		transform.position = p;
	}

}

}
