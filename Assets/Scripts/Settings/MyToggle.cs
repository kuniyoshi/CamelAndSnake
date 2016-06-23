using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Settings
{

public class MyToggle : MonoBehaviour
{

	Configuration.CamelType camelType;
	Configuration.LetterCase letterCase;

	int TheBoolBool;
	int ThePressedTrigger;
	int TheNormalTrigger;
	Toggle toggle;
	Animator animator;
	OnValueChange onValueChange;

	public void Subscribe(OnValueChange listener)
	{
		onValueChange += listener;
	}

	public void GetConfiguration(out Configuration.CamelType camelType)
	{
		camelType = this.camelType;
	}

	public void GetConfiguration(out Configuration.LetterCase letterCase)
	{
		letterCase = this.letterCase;
	}
	
	void Awake()
	{
		TheBoolBool = Animator.StringToHash ("Bool");
		ThePressedTrigger = Animator.StringToHash ("Pressed");
		TheNormalTrigger = Animator.StringToHash ("Normal");

		toggle = GetComponent<Toggle> ();
		Debug.Assert (toggle);
		animator = GetComponent<Animator> ();
		Debug.Assert (animator);
		
		Debug.Assert (Test.Util.HasAnimatorParameter (animator, TheBoolBool));
		Debug.Assert (Test.Util.HasAnimatorParameter (animator, ThePressedTrigger));
		Debug.Assert (Test.Util.HasAnimatorParameter (animator, TheNormalTrigger));

		toggle.onValueChanged.AddListener (SetBool);
		toggle.onValueChanged.AddListener (FlashDeflation);
		toggle.onValueChanged.AddListener (Invoke);

		animator.SetBool (TheBoolBool, toggle.isOn);
		animator.SetTrigger (TheNormalTrigger);

		ConfigurationCamelType ct = GetComponent<ConfigurationCamelType> ();

		if (ct)
		{
			camelType = ct.camelType;
		}

		ConfigurationLetterCase lc = GetComponent<ConfigurationLetterCase> ();

		if (lc)
		{
			letterCase = lc.letterCase;
		}
	}

	void SetBool(bool newValue)
	{
		animator.SetBool (TheBoolBool, newValue);
	}

	void FlashDeflation(bool newValue)
	{
		animator.SetTrigger (newValue ? ThePressedTrigger : TheNormalTrigger);
	}

	void Invoke(bool _newValue)
	{
		if (onValueChange != null)
		{
			onValueChange (this);
		}
	}

}

}
