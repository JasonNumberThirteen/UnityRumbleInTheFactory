using UnityEngine;

[RequireComponent(typeof(NukeEntity), typeof(SpriteRenderer))]
public class NukeRenderer : MonoBehaviour
{
	[SerializeField] private Sprite destroyedNukeSprite;

	private NukeEntity nukeEntity;
	private SpriteRenderer spriteRenderer;

	private void Awake()
	{
		nukeEntity = GetComponent<NukeEntity>();
		spriteRenderer = GetComponent<SpriteRenderer>();

		RegisterToListeners(true);
	}

	private void OnDestroy()
	{
		RegisterToListeners(false);
	}

	private void RegisterToListeners(bool register)
	{
		if(register)
		{
			nukeEntity.nukeDestroyedEvent.AddListener(OnNukeDestroyed);
		}
		else
		{
			nukeEntity.nukeDestroyedEvent.RemoveListener(OnNukeDestroyed);
		}
	}

	private void OnNukeDestroyed()
	{
		if(!RendererHasSetSprite(destroyedNukeSprite))
		{
			spriteRenderer.sprite = destroyedNukeSprite;
		}
	}

	private bool RendererHasSetSprite(Sprite sprite)
	{
		var spriteInRenderer = spriteRenderer.sprite;

		return sprite != null && spriteInRenderer != null && spriteInRenderer == sprite;
	}
}