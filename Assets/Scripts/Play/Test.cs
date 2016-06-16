using System.Collections;
using UnityEngine;

namespace Play
{

public class Test : MonoBehaviour
{

	void Start()
	{
		TextMesh mesh = GetComponent <TextMesh> ();
		Renderer renderer = mesh.renderer;
		Bounds bounds = renderer.bounds;
		Debug.Log ("bounds: " + bounds);
	}

	void Update()
	{
		
	}


}

}
