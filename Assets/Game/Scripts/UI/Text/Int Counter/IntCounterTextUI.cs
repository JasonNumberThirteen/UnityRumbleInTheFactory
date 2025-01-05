using UnityEngine;

[RequireComponent(typeof(IntCounter))]
public class IntCounterTextUI : TextUI
{
	[SerializeField] private string header;
	[SerializeField] private bool addSpaceAfterHeader = true;
	
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
			intCounter.valueChangedEvent.AddListener(OnValueChanged);
		}
		else
		{
			intCounter.valueChangedEvent.RemoveListener(OnValueChanged);
		}
	}

	private void OnValueChanged()
	{
		text.text = GetFormattedText();
	}
	
	private string GetFormattedText()
	{
		var value = GetCounterValueAsString();
		
		if(!string.IsNullOrEmpty(header))
		{
			return addSpaceAfterHeader ? $"{header} {value}" : header + value;
		}
		
		return value;
	}
}