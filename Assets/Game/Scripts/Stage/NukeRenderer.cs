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
		}
	}

	private void Awake() => spriteRenderer = GetComponent<SpriteRenderer>();
	private bool SpriteIsDifferent() => spriteRenderer.sprite != destroyState;
	private void SetSprite() => spriteRenderer.sprite = destroyState;
}