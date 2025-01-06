using UnityEngine;

public class PauseTextUI : TextUI
{
	[SerializeField, Min(0.01f)] private float blinkDelay = 1f;

	private void Update()
	{
		text.enabled = ReachedBlinkDelay();
	}
	
	private bool ReachedBlinkDelay() => Time.unscaledTime % (blinkDelay*2) >= blinkDelay;
}