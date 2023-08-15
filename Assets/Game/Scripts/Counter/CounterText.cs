using TMPro;
using UnityEngine;

public class CounterText : MonoBehaviour
{
	public Counter counter;
	public string header;
	
	private TextMeshProUGUI text;

	public void UpdateText() => text.text = FormattedText();

	private void Awake() => text = GetComponent<TextMeshProUGUI>();
	private string FormattedText() => string.IsNullOrEmpty(header) ? counter.CurrentValue.ToString() : string.Format("{0} {1}", header, counter.CurrentValue);
}