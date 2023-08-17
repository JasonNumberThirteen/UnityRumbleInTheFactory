using TMPro;
using UnityEngine;

public class CounterText : MonoBehaviour
{
	public Counter counter;
	public string header;
	
	private TextMeshProUGUI text;

	public virtual string FormattedCounterValue() => counter.CurrentValue.ToString();
	public void UpdateText() => text.text = FormattedText();

	private void Awake() => text = GetComponent<TextMeshProUGUI>();
	private string FormattedText() => string.IsNullOrEmpty(header) ? FormattedCounterValue() : string.Format("{0} {1}", header, FormattedCounterValue());
}