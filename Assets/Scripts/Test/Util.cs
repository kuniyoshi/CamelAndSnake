using UnityEngine;

namespace Test
{

public class Util
{

	public static bool HasAnimatorParamter(Animator animator, string name)
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

	public static bool HasAnimatorParamter(Animator animator, int id)
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
