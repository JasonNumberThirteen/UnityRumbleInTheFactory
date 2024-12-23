using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class NukeRenderer : MonoBehaviour
{
	public Sprite destroyedNukeSprite;

	private SpriteRenderer spriteRenderer;

	public void ChangeToDestroyedState()
	{
		if(RendererHasSetSprite(destroyedNukeSprite))
		{
			return;
		}
		
		SetSpriteToRenderer(destroyedNukeSprite);
		Destroy(this);
	}

	private void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	private void SetSpriteToRenderer(Sprite sprite)
	{
		spriteRenderer.sprite = sprite;
	}

	private bool RendererHasSetSprite(Sprite sprite)
	{
		var spriteInRenderer = spriteRenderer.sprite;

		return sprite != null && spriteInRenderer != null && spriteInRenderer == sprite;
	}
}