using UnityEngine;

[RequireComponent(typeof(EnemyRobotEntityHealth), typeof(EntityExploder))]
public class EnemyRobotEntity : RobotEntity
{
	public bool HasBonus {get; private set;}
	
	private EnemyRobotEntityHealth enemyRobotEntityHealth;
	private EntityExploder entityExploder;
	
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
		entityExploder = GetComponent<EntityExploder>();

		RegisterToListeners(true);
	}

	private void OnDestroy()
	{
		RegisterToListeners(false);
	}

	private void RegisterToListeners(bool register)
	{
		if(register)
		{
			entityExploder.entityDestroyedEvent.AddListener(OnEntityDestroyed);
		}
		else
		{
			entityExploder.entityDestroyedEvent.RemoveListener(OnEntityDestroyed);
		}
	}

	private void OnEntityDestroyed()
	{
		var stageEventsManager = ObjectMethods.FindComponentOfType<StageEventsManager>();
		
		if(stageEventsManager != null)
		{
			stageEventsManager.SendEvent(StageEventType.EnemyDestroyed, gameObject);
		}
	}
}