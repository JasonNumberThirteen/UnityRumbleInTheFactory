using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BonusEnemyRobotEntityRendererColorAdjuster : MonoBehaviour
{
	private SpriteRenderer spriteRenderer;
	private Color initialColor;
	private float fadeTime;
	private Color targetColor;

	public void Setup(float fadeTime, Color targetColor)
	{
		this.fadeTime = fadeTime;
		this.targetColor = targetColor;
	}

	public void RestoreInitialColor()
	{
		spriteRenderer.color = initialColor;
	}

	private void Update()
	{
		var t = Mathf.PingPong(Time.time, fadeTime);

		spriteRenderer.color = Color.Lerp(initialColor, targetColor, t);
	}

	private void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		initialColor = spriteRenderer.color;
	}
}