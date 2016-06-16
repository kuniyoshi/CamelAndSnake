using System.Collections;
using UnityEngine;

public class SceneStrider : MonoBehaviour
{

	Configuration config;
	bool isStriding;

	public void StartStriding(Configuration newConfig)
	{
		config = newConfig;
		isStriding = true;
	}

	void Start()
	{
		DontDestroyOnLoad (this);
	}

	void Update()
	{
		if (isStriding)
		{
			SceneStrider[] found = Resources.FindObjectsOfTypeAll<SceneStrider> ();

			if (found.Length == 1)
			{
				return;
			}

			SceneStrider target = null;

			for (int i = 0; i < found.Length; i++)
			{
				if (found[i] != this)
				{
					target = found [i];
					break;
				}
			}

			if (!target)
			{
				return;
			}

			target.Set (config);
			isStriding = false;
			Destroy (gameObject);
		}
	}

	void Set(Configuration newConfig)
	{
		config = newConfig;
	}

}
