using UnityEngine;

namespace Title
{

public class DelegateChain
{

	Title.OnSceneChange[] delegations;
	int size;
	int currentIndex;

	public DelegateChain(int requiredSize)
	{
		size = requiredSize;
		delegations = new Title.OnSceneChange[size];
		currentIndex = 0;
	}

	public void Add(int to, Title.OnSceneChange what)
	{
		Debug.Assert(to >= 0 && to < size);
		delegations [to] += what;
	}

	public void StepOver(object o, System.EventArgs arg)
	{
		Debug.Assert (currentIndex < size);

		if (delegations[currentIndex] != null)
		{
			delegations [currentIndex] (o, arg);
			++currentIndex;
		}
	}

	public bool HasNext()
	{
		return currentIndex < size;
	}

	public void StepOut(object o, System.EventArgs arg)
	{
		Debug.Assert (currentIndex < size);

		while (HasNext())
		{
			StepOver (o, arg);
		}
	}

}

}
