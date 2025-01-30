using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerRobotEntityRendererBlinker : MonoBehaviour
{
	private SpriteRenderer spriteRenderer;

	private IEnumerator coroutine;
	private float blinkTime;
	private bool isBlinking;

	public void SetBlinkActive(bool active, float blinkTime)
	{
		this.blinkTime = blinkTime;

		AdjustRendererEnabledState(active);
		SetBlinkingState(active);
	}

	private void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		coroutine = Blink();
	}

	private IEnumerator Blink()
	{
		while (true)
		{
			yield return new WaitForSeconds(blinkTime);

			spriteRenderer.enabled = !spriteRenderer.enabled;
		}
	}

	private void AdjustRendererEnabledState(bool enabled)
	{
		if(!enabled && !spriteRenderer.enabled)
		{
			spriteRenderer.enabled = true;
		}
		else if(enabled && spriteRenderer.enabled)
		{
			spriteRenderer.enabled = false;
		}
	}

	private void SetBlinkingState(bool active)
	{
		if(active && !isBlinking)
		{
			StartCoroutine(coroutine);

			isBlinking = true;
		}
		else if(!active && isBlinking)
		{
			StopCoroutine(coroutine);

			isBlinking = false;
		}
	}
}