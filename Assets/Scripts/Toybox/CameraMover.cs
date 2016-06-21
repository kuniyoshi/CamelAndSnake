using System.Collections;
using UnityEngine;

namespace Toybox
{

public class CameraMover : MonoBehaviour
{

	public float y;

	Canvas canvas;
	Camera scrollCamera;

	void Awake()
	{
		canvas = GetComponentInChildren<Canvas> ();
		Debug.Assert (canvas);
		scrollCamera = GetComponent<Camera> ();
		Debug.Assert (scrollCamera);
	}

	void Start()
	{
		canvas.renderMode = RenderMode.WorldSpace;
		Vector3 pos = canvas.transform.position;
		pos.z = 1.9f;
		canvas.transform.position = pos;

		scrollCamera.fieldOfView = 38f;
	}

	void Update()
	{
		// -5.3f;

		float minY = -5.3f;
		float maxY = 5.3f;

		float currentY = (maxY - minY) * y + minY;
		Vector3 pos = canvas.transform.position;
		pos.y = currentY;
		canvas.transform.position = pos;
	}

}

}
