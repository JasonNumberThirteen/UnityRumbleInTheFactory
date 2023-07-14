using UnityEngine;

public class FortressMetalRenderer : MonoBehaviour
{
	public Sprite bricksTile;
	[Min(0.01f)] public float blinkDelay;
	
	private SpriteRenderer spriteRenderer;
	private Timer timer;
	private Sprite initialSprite;

	private void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		timer = GetComponent<Timer>();
		initialSprite = spriteRenderer.sprite;
	}

	private void Update()
	{
		if(timer.ProgressPercent() >= 0.75f && ReachedBlinkDelay())
		{
			spriteRenderer.sprite = (spriteRenderer.sprite == initialSprite) ? bricksTile : initialSprite;
		}
	}

	private bool ReachedBlinkDelay() => Time.timeSinceLevelLoad % (blinkDelay*2) >= blinkDelay;
}