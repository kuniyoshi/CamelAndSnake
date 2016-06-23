using System.IO;
using System.Text;
using UnityEngine;

public class Vocabulary
{

	string[] phrases;
	string currentPhrase;
	int countOfWords;
	int countOfLetters;

	public Vocabulary()
	{
		string filename = Application.dataPath + @"/data/phrases.txt";
		phrases = File.ReadAllLines (filename);
	}

	public int CountOfCurrentLetters { get { return countOfLetters; } }

	public int CountOfCurrentWords { get { return countOfWords; } }
	
	public string CurrentPhrase { get { return currentPhrase; } }

	public string NextCamel()
	{
		return Camelize (NextSnake ());
	}

	public string NextPascal()
	{
		return Camelize (NextSnake (), true);
	}

	public string NextSnake()
	{
		currentPhrase = phrases [Random.Range (0, phrases.Length)];

		countOfWords = 1;

		for (int i = 0; i < currentPhrase.Length; i++)
		{
			if (currentPhrase[i] == '_')
			{
				++countOfWords;
			}
		}

		countOfLetters = currentPhrase.Length;
		return currentPhrase;
	}

	string Camelize(string snake, bool isPascal = false)
	{
		StringBuilder builder = new StringBuilder ();
		bool willBeUpper = isPascal;

		for (int i = 0; i < snake.Length; i++)
		{
			if (snake[i] == '_')
			{
				willBeUpper = true;
				continue;
			}

			if (!willBeUpper)
			{
				builder.Append (snake [i]);
			}
			else
			{
				builder.Append (snake [i].ToString ().ToUpper ());
				willBeUpper = false;
			}
		}

		return builder.ToString ();
	}

}
