using UnityEngine;

namespace Title
{

public class TouchToStart : MonoBehaviour
{
	Animator animator;

	public bool DidComplete()
	{
		return animator.GetCurrentAnimatorStateInfo (0).shortNameHash == TheAnimatorId.Instance ().TheLastState;
	}

	public void InflateOnce()
	{
		animator.SetTrigger (TheAnimatorId.Instance ().OnSceneChange);
	}

	void Start()
	{
		animator = GetComponent<Animator> ();
		Debug.Assert (animator);
		Debug.Assert (Test.Util.HasAnimatorParameter (animator, TheAnimatorId.Instance (true).OnSceneChange));
		Debug.Assert (animator.HasState (0, TheAnimatorId.Instance ().TheLastState));
	}

}

}
