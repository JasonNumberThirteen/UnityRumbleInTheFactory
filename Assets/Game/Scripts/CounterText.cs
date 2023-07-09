using TMPro;
using UnityEngine;

public class CounterText : MonoBehaviour
{
	public Counter counter;
	public string header;
	
	private TextMeshProUGUI text;

	public void UpdateText() => text.text = string.Format("{0} {1}", header, counter.CurrentValue);

	private void Awake() => text = GetComponent<TextMeshProUGUI>();
}