using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class BlinkingTextUI : TextUI
{
	[SerializeField, Min(0.01f)] private float blinkDelay = 1f;

	private void Update()
	{
		text.enabled = ReachedBlinkDelay();
	}
	
	private bool ReachedBlinkDelay() => Time.unscaledTime % (blinkDelay*2) >= blinkDelay;
}