using System.Collections;
using UnityEngine;

public class CharacterAnimation
{

	// static
	struct Child
	{
		public float x;
		public Animator animator;
		public TextMesh textMesh;
		public Transform transform;
		public float Z { set { textMesh.offsetZ = value; } }
	}

	static float MagicFactor = 0.1f;
	static float MagicDelta = 10f * MagicFactor;

	// field
	TextMesh textMesh;
	Child[] children;
	Transform parentTransform;
	string text_;
	float halfHeight;

	// special member
	public CharacterAnimation (Transform transform, TextMesh mesh, GameObject[] objects)
	{
		Debug.Assert (transform);
		Debug.Assert (mesh);
		Debug.Assert (objects != null);
		parentTransform = transform;
		textMesh = mesh;
		SetupChildren(objects);
		SetHalfHeight (mesh.text);
	}

	// public property
	public float HalfHeight { get { return halfHeight; } }

	public string Text
	{
		get { return text_; }
		set
		{
			text_ = value;
			SetupChildText ();
		}
	}

	public float Z { get { return textMesh.offsetZ; } private set { textMesh.offsetZ = value; } }

	// protected property
	protected int Length { get { return text_.Length; } }

	// public member
	public void SetHalfHeight(string text)
	{
		halfHeight = GetHalfHeight (text);
	}

	public void WarpTo(Vector3 newPosition)
	{
		Z = newPosition.z;
		newPosition.z = 0f;
		newPosition.y = newPosition.y + halfHeight + halfHeight * MagicDelta;
		parentTransform.position = newPosition;
	}

	// protected member
	protected void SetTriggerTo(int trigger, int to)
	{
		Debug.Assert (to < text_.Length);
		children [to].animator.SetTrigger (trigger);
	}

	protected bool TestAnimatorHasTrigger(int id)
	{
		return Test.Util.HasAnimatorParameter (children [0].animator, id);
	}

	// private member
	float GetHalfHeight(string text)
	{
		CharacterInfo info;
		string backupString = textMesh.text;
		textMesh.text = text;
		textMesh.font.GetCharacterInfo (	text [0],
											out info,
											textMesh.font.fontSize,
											textMesh.fontStyle);
		textMesh.text = backupString;
		float halfHeight = (info.maxY - info.minY) * MagicFactor * 0.5f;
		return halfHeight;
	}

	void SetupChildren(GameObject[] objects)
	{
		Debug.Assert (children == null);
		children = new Child[objects.Length];

		for (int i = 0; i < children.Length; i++)
		{
			GameObject child = objects [i];
			children [i].animator = child.GetComponentInChildren<Animator> ();
			children [i].animator.enabled = true;
			Debug.Assert (children [i].animator);
			children [i].textMesh = child.GetComponentInChildren<TextMesh> ();
			Debug.Assert (children [i].textMesh);
			children [i].textMesh.text = "";
			children [i].transform = child.transform;
			children [i].transform.parent = parentTransform;
			child.name = "bit." + i;
		}
	}

	void SetupChildText()
	{
		float offset = parentTransform.position.x;

		CharacterInfo info = new CharacterInfo ();
		Vector3 childPosition = new Vector3 (0f, parentTransform.position.y, 0f);
		Font font = textMesh.font;
		int fontSize = font.fontSize;
		FontStyle fontStyle = textMesh.fontStyle;

		for (int i = 0; i < text_.Length; i++)
		{
			children [i].textMesh.text = text_ [i].ToString ();
			childPosition.x = offset;
			children [i].Z = Z;
			children [i].transform.position = childPosition;

			font.GetCharacterInfo (text_ [i], out info, fontSize, fontStyle);
			offset += info.advance * MagicFactor;
		}
	}

}
