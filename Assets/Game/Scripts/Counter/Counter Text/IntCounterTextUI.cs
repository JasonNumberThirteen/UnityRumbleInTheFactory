using TMPro;
using UnityEngine;

[RequireComponent(typeof(IntCounter), typeof(TextMeshProUGUI))]
public class IntCounterTextUI : MonoBehaviour
{
	[SerializeField] private string header;
	[SerializeField] private bool addSpaceAfterHeader = true;
	
	private IntCounter intCounter;
	private TextMeshProUGUI text;

	public virtual string GetCounterValueAsString() => intCounter.CurrentValue.ToString();
	
	private void Awake()
	{
		intCounter = GetComponent<IntCounter>();
		text = GetComponent<TextMeshProUGUI>();

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