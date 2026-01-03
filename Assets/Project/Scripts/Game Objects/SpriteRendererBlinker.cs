using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteRendererBlinker : MonoBehaviour
{
	[SerializeField, Min(0.01f)] private float blinkDelay = 1f;
	
	private SpriteRenderer spriteRenderer;

	private void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	private void Start()
	{
		InvokeRepeating(nameof(SwitchRendererEnabled), blinkDelay, blinkDelay);
	}

	private void SwitchRendererEnabled()
	{
		spriteRenderer.enabled = !spriteRenderer.enabled;
	}
}