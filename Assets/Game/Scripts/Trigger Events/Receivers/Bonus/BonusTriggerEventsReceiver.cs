using UnityEngine;

public abstract class BonusTriggerEventsReceiver : MonoBehaviour, ITriggerableOnEnter
{
	protected StageSoundManager stageSoundManager;
	protected PlayerRobotsDataManager playerRobotsDataManager;

	[SerializeField] private GameData gameData;
	[SerializeField, Min(0)] private int points = 500;

	private StageStateManager stageStateManager;
	
	public void TriggerOnEnter(GameObject sender)
	{
		if(stageStateManager == null || stageStateManager.GameIsOver())
		{
			return;
		}

		if(sender.TryGetComponent(out EnemyRobotEntity _) && !BonusCanBeCollectedByEnemy())
		{
			return;
		}
		
		OnBonusCollected(sender);
		Destroy(gameObject);
	}

	protected abstract void GiveEffect(GameObject sender);
	protected virtual bool ShouldPlaySound(GameObject sender) => true;

	protected virtual void Awake()
	{
		stageSoundManager = ObjectMethods.FindComponentOfType<StageSoundManager>();
		playerRobotsDataManager = ObjectMethods.FindComponentOfType<PlayerRobotsDataManager>();
		stageStateManager = ObjectMethods.FindComponentOfType<StageStateManager>();
	}

	private void OnBonusCollected(GameObject sender)
	{
		GiveEffect(sender);
		AddPointsToPlayerIfPossible(sender);
		PlaySound(sender);
	}

	private void AddPointsToPlayerIfPossible(GameObject sender)
	{
		if(!sender.TryGetComponent(out PlayerRobotEntity playerRobotEntity))
		{
			return;
		}
		
		var playerData = playerRobotEntity.GetPlayerRobotData();

		if(playerRobotsDataManager != null && playerData != null)
		{
			playerRobotsDataManager.ModifyScore(playerData, points, gameObject);
		}
	}

	private void PlaySound(GameObject sender)
	{
		if(stageSoundManager != null && ShouldPlaySound(sender))
		{
			stageSoundManager.PlaySound(SoundEffectType.BonusCollect);
		}
	}

	private bool BonusCanBeCollectedByEnemy() => GameDataMethods.GetDifficultyTierValue(gameData, tier => tier.EnemiesCanCollectBonuses());
}