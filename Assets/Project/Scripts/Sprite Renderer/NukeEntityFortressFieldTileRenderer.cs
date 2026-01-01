using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class NukeEntityFortressFieldTileRenderer : MonoBehaviour
{
	private SpriteRenderer spriteRenderer;
	private Sprite initialSprite;
	private Sprite spriteToSwitch;

	public void Setup(float lifetime, float timeForBlinkStart, float blinkDuration, Sprite spriteToSwitch)
	{
		this.spriteToSwitch = spriteToSwitch;

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
		
		spriteRenderer.sprite = rendererHasInitialSprite ? spriteToSwitch : initialSprite;
	}
}