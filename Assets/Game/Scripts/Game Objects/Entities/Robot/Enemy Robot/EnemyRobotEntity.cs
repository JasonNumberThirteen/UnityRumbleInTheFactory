using UnityEngine;

[RequireComponent(typeof(EnemyRobotEntityHealth))]
public class EnemyRobotEntity : RobotEntity
{
	public bool HasBonus {get; private set;}
	
	private EnemyRobotEntityHealth enemyRobotEntityHealth;
	private EnemyRobotEntitySpawnManager enemyRobotEntitySpawnManager;
	
	public override bool IsFriendly() => false;

	public override void OnLifeBonusCollected(int lives)
	{
		enemyRobotEntityHealth.IncreaseHealthBy(lives);
	}

	public void SetupForBonusType(float fadeTime, Color targetColor)
	{
		HasBonus = true;

		var bonusEnemyRobotEntityRendererColorAdjuster = gameObject.AddComponent<BonusEnemyRobotEntityRendererColorAdjuster>();

		bonusEnemyRobotEntityRendererColorAdjuster.Setup(fadeTime, targetColor);
	}

	public void RemoveBonusTypeProperties()
	{
		HasBonus = false;
		
		if(TryGetComponent(out BonusEnemyRobotEntityRendererColorAdjuster bonusEnemyRobotEntityRendererColorAdjuster))
		{
			bonusEnemyRobotEntityRendererColorAdjuster.RestoreInitialColor();
			Destroy(bonusEnemyRobotEntityRendererColorAdjuster);
		}
	}

	protected override void Awake()
	{
		base.Awake();

		enemyRobotEntityHealth = GetComponent<EnemyRobotEntityHealth>();
		enemyRobotEntitySpawnManager = ObjectMethods.FindComponentOfType<EnemyRobotEntitySpawnManager>();
	}

	private void OnDestroy()
	{
		if(enemyRobotEntitySpawnManager != null)
		{
			enemyRobotEntitySpawnManager.CountDefeatedEnemy();
		}
	}
}