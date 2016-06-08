using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Title
{

public class Title : MonoBehaviour
{
	public float delay = 2f;
	float startAt = 0f;
	Animator animator;

	void Start()
	{
		animator = GetComponent<Animator> ();
		animator.enabled = false;

		startAt = Time.realtimeSinceStartup;

		// Hide the text, but default alpha is 1f.  It because I want to see what is wrote the text in Scene.
		Text text = GetComponent<Text> ();
		Color color = text.color;
		color.a = 0f;
		text.color = color;
	}

	void Update()
	{
		if (!animator.enabled && (Time.realtimeSinceStartup - startAt) > delay)
		{
			animator.enabled = true;
		}
	}
}

}
