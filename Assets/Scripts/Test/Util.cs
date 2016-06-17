using UnityEngine;

namespace Test
{

public class Util
{

	public static bool HasAnimatorParameter(Animator animator, string name)
	{
		foreach (var paramter in animator.parameters)
		{
			if (paramter.name == name)
			{
				return true;
			}
		}

		return false;
	}

	public static bool HasAnimatorParameter(Animator animator, int id)
	{
		foreach (var paramter in animator.parameters)
		{
			if (paramter.nameHash == id)
			{
				return true;
			}
		}

		return false;
	}

}

}
