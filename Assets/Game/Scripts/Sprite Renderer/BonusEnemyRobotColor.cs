using UnityEngine;

public class BonusEnemyRobotColor : MonoBehaviour
{
	private SpriteRenderer spriteRenderer;
	private Color initialColor;
	private BonusSpawnManager bonusSpawnManager;

	public void RestoreInitialColor() => spriteRenderer.color = initialColor;

	private void Update() => LerpColor();

	private void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		initialColor = spriteRenderer.color;
		bonusSpawnManager = FindAnyObjectByType<BonusSpawnManager>();
	}

	private void LerpColor()
	{
		float t = Mathf.PingPong(Time.time, bonusSpawnManager.GetBonusEnemyColorFadeTime());
		Color color = Color.Lerp(initialColor, bonusSpawnManager.GetBonusEnemyColor(), t);

		spriteRenderer.color = color;
	}
}