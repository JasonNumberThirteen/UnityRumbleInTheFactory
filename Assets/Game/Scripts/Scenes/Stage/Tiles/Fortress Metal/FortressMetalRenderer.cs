using UnityEngine;

public class FortressMetalRenderer : MonoBehaviour
{
	public Sprite bricksTile;
	[Min(0.01f)] public float blinkDelay = 5f;
	[Min(0.01f)] public float blinkDuration = 1f;
	
	private SpriteRenderer spriteRenderer;
	private Sprite initialSprite;

	public void SwapSprite() => spriteRenderer.sprite = (spriteRenderer.sprite == initialSprite) ? bricksTile : initialSprite;

	private void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		initialSprite = spriteRenderer.sprite;
	}

	private void Start() => InvokeRepeating("SwapSprite", blinkDelay, blinkDuration);
}