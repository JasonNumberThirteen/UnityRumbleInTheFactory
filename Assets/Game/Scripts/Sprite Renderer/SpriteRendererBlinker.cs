using UnityEngine;

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
		InvokeRepeating(nameof(SwitchEnable), blinkDelay, blinkDelay);
	}

	private void SwitchEnable()
	{
		spriteRenderer.enabled = !spriteRenderer.enabled;
	}
}