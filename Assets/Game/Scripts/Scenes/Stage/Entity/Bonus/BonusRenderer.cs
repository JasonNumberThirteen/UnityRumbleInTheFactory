using UnityEngine;

public class BonusRenderer : MonoBehaviour
{
	[Min(0.01f)] public float blinkDelay = 1f;
	
	private SpriteRenderer spriteRenderer;

	private void Awake() => spriteRenderer = GetComponent<SpriteRenderer>();
	private void Update() => spriteRenderer.enabled = ReachedBlinkDelay();
	private bool ReachedBlinkDelay() => Time.timeSinceLevelLoad % (blinkDelay*2) >= blinkDelay;
}