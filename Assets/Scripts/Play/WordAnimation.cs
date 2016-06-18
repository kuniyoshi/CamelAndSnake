using System.Collections;
using UnityEngine;

namespace Play
{

public class WordAnimation : MonoBehaviour
{

	struct Child
	{
		public float x;
		public Animator animator;
		public TextMesh textMesh;
		public Transform transform;
		public float Z { set { textMesh.offsetZ = value; } }
	}

	static int TheShowTrigger;
	static int TheHideTrigger;
	static float MagicFactor = 0.1f;
	static float MagicDelta = 12 * MagicFactor;

	public GameObject bitPrefab;
//	public float duration = 2f;

	TextMesh textMesh;
	string word_;
	Child[] children;
	float height;

	public string Word
	{
		get { return word_; }
		set
		{
			Debug.Assert (children != null && value.Length < children.Length);
			word_ = value;
			Init ();
		}
	}

	public float Z { get { return textMesh.offsetZ; } private set { textMesh.offsetZ = value; } }

	public void Hide()
	{
		for (int i = 0; i < word_.Length; i++)
		{
			children [i].animator.SetTrigger (TheHideTrigger);
		}
	}

	public void Show()
	{
		for (int i = 0; i < word_.Length; i++)
		{
			children [i].animator.SetTrigger (TheShowTrigger);
		}
	}

	public void SetupBuffer(int size)
	{
		Debug.Assert (children == null);
		children = new Child[size];

		bool didAnimatorTest = false;

		for (int i = 0; i < children.Length; i++)
		{
			GameObject anObject = Instantiate (bitPrefab);
			children [i].animator = anObject.GetComponent<Animator> ();
			Debug.Assert (children [i].animator);
			children [i].textMesh = anObject.GetComponent<TextMesh> ();
			Debug.Assert (children [i].textMesh);
			children [i].textMesh.text = "";
			children [i].transform = anObject.transform;
			anObject.name = "bit." + i;
			anObject.transform.parent = transform;

			if (didAnimatorTest)
			{
				continue;
			}

			Debug.Assert (Test.Util.HasAnimatorParameter (children [i].animator, TheShowTrigger));
			Debug.Assert (Test.Util.HasAnimatorParameter (children [i].animator, TheHideTrigger));
			didAnimatorTest = true;
		}
	}

	public void WarpTo(Vector3 newPosition)
	{
		Z = newPosition.z;
		newPosition.z = 0f;
		newPosition.y = newPosition.y + height + MagicDelta;
		transform.position = newPosition;
	}

	void Awake()
	{
		Debug.Assert (bitPrefab);
	}

	void Start()
	{
		TheShowTrigger = Animator.StringToHash ("Show"); // test at SetupBuffer
		TheHideTrigger = Animator.StringToHash ("Hide"); // test at SetupBuffer
		textMesh = GetComponent<TextMesh> ();
		Debug.Assert (textMesh);

		CharacterInfo info;
		string backupString = textMesh.text;
		textMesh.text = "'Igjpqy";
		textMesh.font.GetCharacterInfo ('I', out info, textMesh.font.fontSize, textMesh.fontStyle);
		textMesh.text = backupString;
		height = (info.maxY - info.minY) * MagicFactor * 0.5f;
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

//	IEnumerator ShowCharacter()
//	{
//		for (int i = 0; i < animators.Length; i++)
//		{
//			animators [i].SetTrigger ("Show");
//			yield return null; //new WaitForSeconds(0.05f);
//		}
//	}

//	IEnumerator HideCharacter()
//	{
//		for (int i = 0; i < animators.Length; i++)
//		{
//			animators [i].SetTrigger ("Hide");
//			yield return null; //new WaitForSeconds(0.05f);
//		}
//	}

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

	void Init()
	{
		float offset = transform.position.x;

		CharacterInfo info = new CharacterInfo ();
		Vector3 childPosition = new Vector3 (0f, transform.position.y, 0f);
		Font font = textMesh.font;
		int fontSize = font.fontSize;
		FontStyle fontStyle = textMesh.fontStyle;

		for (int i = 0; i < word_.Length; i++)
		{
			children [i].textMesh.text = word_ [i].ToString ();
			childPosition.x = offset;
			children [i].Z = Z;
			children [i].transform.position = childPosition;

			font.GetCharacterInfo (word_ [i], out info, fontSize, fontStyle);
			offset += info.advance * MagicFactor;
		}
	}
}

}
