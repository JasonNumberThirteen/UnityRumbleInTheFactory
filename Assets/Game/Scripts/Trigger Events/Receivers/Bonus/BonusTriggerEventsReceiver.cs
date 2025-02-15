using UnityEngine;

public abstract class BonusTriggerEventsReceiver : MonoBehaviour, ITriggerableOnEnter
{
	protected StageSoundManager stageSoundManager;

	[SerializeField] private GameData gameData;
	[SerializeField, Min(0)] private int points = 500;

	protected PlayerRobotsDataManager playerRobotsDataManager;

	private StageStateManager stageStateManager;
	
	public void TriggerOnEnter(GameObject sender)
	{
		if(stageStateManager == null || stageStateManager.GameIsOver())
		{
			return;
		}
		
		if(BonusCanBeCollectedByEnemy(sender))
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
		stageStateManager = ObjectMethods.FindComponentOfType<StageStateManager>();
		playerRobotsDataManager = ObjectMethods.FindComponentOfType<PlayerRobotsDataManager>();
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

	private bool BonusCanBeCollectedByEnemy(GameObject sender) => sender.TryGetComponent(out EnemyRobotEntity _) && GameDataMethods.GetDifficultyTierValue(gameData, tier => tier.EnemiesCanCollectBonuses());
}