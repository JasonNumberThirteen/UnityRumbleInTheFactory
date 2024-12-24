using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextUIBlinker : MonoBehaviour
{
	[SerializeField, Min(0.01f)] private float blinkDelay = 1f;
	
	private TextMeshProUGUI text;

	private void Awake()
	{
		text = GetComponent<TextMeshProUGUI>();
	}

	private void Update()
	{
		text.enabled = ReachedBlinkDelay();
	}
	
	private bool ReachedBlinkDelay() => Time.unscaledTime % (blinkDelay*2) >= blinkDelay;
}