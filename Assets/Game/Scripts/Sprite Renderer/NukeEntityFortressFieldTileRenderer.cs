using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class NukeEntityFortressFieldTileRenderer : MonoBehaviour
{
	private SpriteRenderer spriteRenderer;
	private Sprite initialSprite;
	private Sprite spriteToBlink;

	public void Setup(float lifetime, float timeForBlinkStart, float blinkDuration, Sprite spriteToBlink)
	{
		this.spriteToBlink = spriteToBlink;

		InvokeRepeating(nameof(SwitchSprite), lifetime - timeForBlinkStart, blinkDuration);
	}

	private void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		initialSprite = spriteRenderer.sprite;
	}

	private void SwitchSprite()
	{
		var rendererHasInitialSprite = spriteRenderer.sprite == initialSprite;
		
		spriteRenderer.sprite = rendererHasInitialSprite ? spriteToBlink : initialSprite;
	}
}