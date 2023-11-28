using UnityEngine;

public class BonusEnemyRobotColor : MonoBehaviour
{
	private SpriteRenderer spriteRenderer;
	private Color initialColor;
	private EnemySpawnManager enemySpawnManager;

	public void RestoreInitialColor() => spriteRenderer.color = initialColor;

	private void Update() => LerpColor();

	private void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		initialColor = spriteRenderer.color;
		enemySpawnManager = StageManager.instance.enemySpawnManager;
	}

	private void LerpColor()
	{
		float t = Mathf.PingPong(Time.time, enemySpawnManager.bonusEnemyColorFadeTime);
		Color color = Color.Lerp(initialColor, enemySpawnManager.bonusEnemyColor, t);

		spriteRenderer.color = color;
	}
}