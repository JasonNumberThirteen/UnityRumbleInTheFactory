using UnityEngine;

[RequireComponent(typeof(EnemyRobotEntityHealth))]
public class EnemyRobotEntity : RobotEntity
{
	public bool HasBonus {get; private set;}
	
	private EnemyRobotEntityHealth enemyRobotEntityHealth;
	
	public override bool IsFriendly() => false;

	protected override StageEventType GetStageEventTypeOnDestructionEvent() => StageEventType.EnemyWasDestroyed;

	public override void OnLifeBonusCollected(int lives)
	{
		enemyRobotEntityHealth.ModifyCurrentHealthBy(lives);
	}

	public void SetupForBonusType(Color targetColor, float fadeTime)
	{
		HasBonus = true;

		var bonusEnemyRobotEntityRendererColorAdjuster = gameObject.AddComponent<BonusEnemyRobotEntityRendererColorAdjuster>();

		bonusEnemyRobotEntityRendererColorAdjuster.Setup(targetColor, fadeTime);
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
		enemyRobotEntityHealth = GetComponent<EnemyRobotEntityHealth>();

		base.Awake();
	}
}