using UnityEngine;

public class BonusEnemyRobotColor : MonoBehaviour
{
	private SpriteRenderer spriteRenderer;
	private Color initialColor;
	private EnemySpawnManager enemySpawnManager;

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
		Color color = Color.Lerp(initialColor, enemySpawnManager.bonusEnemyTargetColor, t);

		spriteRenderer.color = color;
	}
}