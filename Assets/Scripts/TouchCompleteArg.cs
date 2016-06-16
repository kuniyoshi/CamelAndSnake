using UnityEngine;

public class TouchCompleteArg : System.EventArgs
{

	Vector3 position;
	float completedAt;

	public TouchCompleteArg(Vector3 pos, float at)
	{
		position = pos;
		completedAt = at;
	}

	public Vector3 Position { get { return position; } }

	public float CompletedAt { get { return completedAt; } }

}
