using TMPro;
using UnityEngine;

[RequireComponent(typeof(IntCounter), typeof(TextMeshProUGUI))]
public class IntCounterTextUI : MonoBehaviour
{
	[SerializeField] private string header;
	[SerializeField] private bool addSpaceAfterHeader = true;
	
	private IntCounter intCounter;
	private TextMeshProUGUI text;

	public virtual string GetFormattedCounterValue() => intCounter.CurrentValue.ToString();
	
	public void UpdateText()
	{
		text.text = GetFormattedText();
	}

	private void Awake()
	{
		intCounter = GetComponent<IntCounter>();
		text = GetComponent<TextMeshProUGUI>();
	}
	
	private string GetFormattedText()
	{
		var value = GetFormattedCounterValue();
		
		if(!string.IsNullOrEmpty(header))
		{
			return addSpaceAfterHeader ? $"{header} {value}" : header + value;
		}
		
		return value;
	}
}