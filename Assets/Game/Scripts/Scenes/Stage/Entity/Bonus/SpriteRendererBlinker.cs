using UnityEngine;

public class SpriteRendererBlinker : MonoBehaviour
{
	[Min(0.01f)] public float blinkDelay = 1f;
	
	private SpriteRenderer spriteRenderer;

	private void Awake() => spriteRenderer = GetComponent<SpriteRenderer>();
	private void Start() => InvokeRepeating(nameof(SwitchRendererEnable), blinkDelay, blinkDelay);
	private void SwitchRendererEnable() => spriteRenderer.enabled = !spriteRenderer.enabled;
}