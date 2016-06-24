using System.Collections;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace TestText
{

public class GameController : MonoBehaviour
{

	public Text text;

	Vocabulary vocabulary;
	delegate string GetString();

	void Awake()
	{
		Debug.Assert (text);
	}

	void Start()
	{
		vocabulary = new Vocabulary ();

		StringBuilder builder = new StringBuilder ();

		for (int i = 0; i < 5; i++)
		{
			builder.Append (vocabulary.NextCamel ());
			builder.Append ('\n');
		}

		text.text = builder.ToString ();
	}

	void Update()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			text.text = SetText (delegate() {
				return vocabulary.NextCamel ();
			});
		}

		if (Input.GetButtonDown("Fire2"))
		{
			text.text = SetText (delegate() {
				return vocabulary.NextSnake ();
			});
		}

		if (Input.GetButtonDown("Fire3"))
		{
			text.text = SetText (delegate() {
				return vocabulary.NextPascal ();
			});
		}
	}

	string SetText(GetString generator)
	{
		StringBuilder builder = new StringBuilder ();

		for (int i = 0; i < 5; i++)
		{
			builder.Append (generator ());
			builder.Append ('\n');
		}

		return builder.ToString ();
	}

}

}
