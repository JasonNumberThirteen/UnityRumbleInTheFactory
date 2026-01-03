using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BonusEnemyRobotEntityRendererColorAdjuster : MonoBehaviour
{
	private SpriteRenderer spriteRenderer;
	private Color initialColor;
	private Color targetColor;
	private float fadeTime;

	public void Setup(Color targetColor, float fadeTime)
	{
		this.targetColor = targetColor;
		this.fadeTime = fadeTime;
	}

	public void RestoreInitialColor()
	{
		SetColorToRenderer(initialColor);
	}

	private void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		initialColor = spriteRenderer.color;
	}

	private void Update()
	{
		var t = Mathf.PingPong(Time.time, fadeTime);
		var color = Color.Lerp(initialColor, targetColor, t);

		SetColorToRenderer(color);
	}

	private void SetColorToRenderer(Color color)
	{
		spriteRenderer.color = color;
	}
}