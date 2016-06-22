using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Result
{

public class SearchLight : MonoBehaviour
{

	public int totalSize = 10;
	public float angleSpeed = -180f;
	public Image searchLightImage;
	public GameObject background;

	Image[] images;
	bool doesVisible;

	public void Hide()
	{
		doesVisible = false;

		for (int i = 0; i < images.Length; i++)
		{
			images [i].enabled = false;
		}

		background.transform.localScale = Vector3.zero;
	}

	public void Show()
	{
		doesVisible = true;

		for (int i = 0; i < images.Length; i++)
		{
			images [i].enabled = true;
		}

		background.transform.localScale = Vector3.one;
	}

	void Awake()
	{
		Debug.Assert (totalSize > 0);
		Debug.Assert (searchLightImage);
		Debug.Assert (background);
	}

	void Start()
	{
		images = new Image[totalSize];
		CloneImage ();
		doesVisible = false;
		Hide ();
	}

	void Update()
	{
		if (!doesVisible)
		{
			return;
		}

		for (int i = 0; i < images.Length; i++)
		{
			Quaternion q = images [i].transform.rotation;
			Vector3 e = q.eulerAngles;
			e.z = e.z + angleSpeed * Time.deltaTime;
			images [i].transform.rotation = Quaternion.Euler (e);
		}
	}

	void CloneImage()
	{
		images [0] = searchLightImage;

		for (int i = 1; i < totalSize; i++)
		{
			Image image;
			image = Instantiate (searchLightImage);
			image.transform.SetParent (transform, false);
			images [i] = image;
			images [i].transform.SetSiblingIndex (0);
		}

		float angle = 360f / (float)totalSize;
		Vector3 euler = new Vector3 (0f, 0f, 0f);

		for (int i = 0; i < images.Length; i++)
		{
			images [i].transform.rotation = Quaternion.Euler (euler);
			euler.z = euler.z + angle;
		}
	}

}

}
