using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Timer))]
public class FortressMetalRenderer : MonoBehaviour
{
	[SerializeField, Min(0.01f)] private float timeForBlinkStart = 5f;
	[SerializeField, Min(0.01f)] private float blinkDuration = 1f;
	[SerializeField] private Sprite bricksTileSprite;
	
	private SpriteRenderer spriteRenderer;
	private Sprite initialSprite;

	private void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		initialSprite = spriteRenderer.sprite;
	}

	private void Start()
	{
		var timer = GetComponent<Timer>();
		var blinkDelay = timer.duration - timeForBlinkStart;
		
		InvokeRepeating(nameof(SwitchSprite), blinkDelay, blinkDuration);
	}

	private void SwitchSprite()
	{
		var rendererHasInitialSprite = spriteRenderer.sprite == initialSprite;
		
		spriteRenderer.sprite = rendererHasInitialSprite ? bricksTileSprite : initialSprite;
	}
}