using UnityEngine;

public class BonusEnemyRobotColor : MonoBehaviour
{
	public Color targetColor;
	[Min(0.01f)] public float fadeTime = 1f;
	
	private SpriteRenderer spriteRenderer;
	private Color initialColor;

	private void Update() => LerpColor();

	private void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		initialColor = spriteRenderer.color;
	}

	private void LerpColor()
	{
		float t = Mathf.PingPong(Time.unscaledTime, fadeTime);
		Color color = Color.Lerp(initialColor, targetColor, t);

		spriteRenderer.color = color;
	}
}