using TMPro;
using UnityEngine;

public class PauseText : MonoBehaviour
{
	[Min(0.01f)] public float blinkDelay = 1f;
	
	private TextMeshProUGUI text;

	private void Awake() => text = GetComponent<TextMeshProUGUI>();
	private void Update() => text.enabled = ReachedBlinkDelay();
	private bool ReachedBlinkDelay() => Time.unscaledTime % (blinkDelay*2) >= blinkDelay;
}