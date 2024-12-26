using TMPro;
using UnityEngine;

public class IntCounterTextUI : MonoBehaviour
{
	public IntCounter counter;
	public string header;
	public bool addSpaceAfterHeader = true;
	
	private TextMeshProUGUI text;

	public virtual string FormattedCounterValue() => counter.CurrentValue.ToString();
	public void UpdateText() => text.text = FormattedText();

	private void Awake() => text = GetComponent<TextMeshProUGUI>();
	
	private string FormattedText()
	{
		string value = FormattedCounterValue();
		
		if(!string.IsNullOrEmpty(header))
		{
			return addSpaceAfterHeader ? string.Format("{0} {1}", header, value) : header + value;
		}
		
		return value;
	}
}