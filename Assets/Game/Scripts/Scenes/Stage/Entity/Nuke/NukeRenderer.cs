using UnityEngine;

public class NukeRenderer : MonoBehaviour
{
	public Sprite destroyState;

	private SpriteRenderer spriteRenderer;

	public void ChangeToDestroyedState()
	{
		if(SpriteIsDifferent())
		{
			SetSprite();
			Destroy(this);
		}
	}

	private void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	private void SetSprite()
	{
		spriteRenderer.sprite = destroyState;
	}

	private bool SpriteIsDifferent() => spriteRenderer.sprite != destroyState;
}