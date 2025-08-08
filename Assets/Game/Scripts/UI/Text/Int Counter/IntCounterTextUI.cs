using UnityEngine;

[DefaultExecutionOrder(-10)]
[RequireComponent(typeof(IntCounter))]
public class IntCounterTextUI : TextUI
{
	[SerializeField] private string header;
	[SerializeField] private bool addSpaceAfterHeader = true;
	[SerializeField] private string postfix;
	[SerializeField] private bool addSpaceAfterPostfix = true;
	
	private IntCounter intCounter;

	public virtual string GetCounterValueAsString() => GetCounterValue().ToString();

	public int GetCounterValue() => intCounter.CurrentValue;
	
	protected override void Awake()
	{
		base.Awake();
		
		intCounter = GetComponent<IntCounter>();

		RegisterToListeners(true);
	}

	private void OnDestroy()
	{
		RegisterToListeners(false);
	}

	private void RegisterToListeners(bool register)
	{
		if(register)
		{
			intCounter.valueWasChangedEvent.AddListener(OnValueWasChanged);
		}
		else
		{
			intCounter.valueWasChangedEvent.RemoveListener(OnValueWasChanged);
		}
	}

	private void OnValueWasChanged()
	{
		text.text = GetFormattedText();
	}
	
	private string GetFormattedText()
	{
		var value = GetCounterValueAsString();
		var headerIsEmpty = string.IsNullOrEmpty(header);
		var postfixIsEmpty = string.IsNullOrEmpty(postfix);

		if(!headerIsEmpty && postfixIsEmpty)
		{
			return addSpaceAfterHeader ? $"{header} {value}" : header + value;
		}
		else if(headerIsEmpty && !postfixIsEmpty)
		{
			return addSpaceAfterPostfix ? $"{value} {postfix}" : value + postfix;
		}
		else if(!headerIsEmpty && !postfixIsEmpty)
		{
			if(!addSpaceAfterHeader && !addSpaceAfterPostfix)
			{
				return header + value + postfix;
			}
			else if(addSpaceAfterHeader && !addSpaceAfterPostfix)
			{
				return $"{header} {value}";
			}
			else if(!addSpaceAfterHeader && addSpaceAfterPostfix)
			{
				return $"{value} {postfix}";
			}
			else if(addSpaceAfterHeader && addSpaceAfterPostfix)
			{
				return $"{header} {value} {postfix}";
			}
		}

		return value;
	}
}