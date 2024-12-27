using UnityEngine;

[RequireComponent(typeof(Nuke), typeof(SpriteRenderer))]
public class NukeRenderer : MonoBehaviour
{
	[SerializeField] private Sprite destroyedNukeSprite;

	private Nuke nuke;
	private SpriteRenderer spriteRenderer;

	private void Awake()
	{
		nuke = GetComponent<Nuke>();
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
			nuke.nukeDestroyedEvent.AddListener(OnNukeDestroyed);
		}
		else
		{
			nuke.nukeDestroyedEvent.RemoveListener(OnNukeDestroyed);
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