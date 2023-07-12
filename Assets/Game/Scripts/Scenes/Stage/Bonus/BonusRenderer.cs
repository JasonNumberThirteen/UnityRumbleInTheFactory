using UnityEngine;

public class BonusRenderer : MonoBehaviour
{
	public float blinkDelay = 1f;
	
	private SpriteRenderer spriteRenderer;

	private void Awake() => spriteRenderer = GetComponent<SpriteRenderer>();
	private void Update() => spriteRenderer.enabled = ReachedBlinkDelay();
	private bool ReachedBlinkDelay() => Time.timeSinceLevelLoad % (blinkDelay*2) >= blinkDelay;
}