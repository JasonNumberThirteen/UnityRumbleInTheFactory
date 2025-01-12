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
		
		if(sender.TryGetComponent(out EnemyRobotEntity _) && gameData != null && !gameData.GetDifficultyTierValue(tier => tier.EnemiesCanCollectBonuses()))
		{
			return;
		}
		
		OnBonusCollected(sender);
		Destroy(gameObject);
	}

	protected abstract void GiveEffect(GameObject sender);

	protected virtual void Awake()
	{
		stageSoundManager = FindAnyObjectByType<StageSoundManager>(FindObjectsInactive.Include);
		stageStateManager = FindAnyObjectByType<StageStateManager>(FindObjectsInactive.Include);
		playerRobotsDataManager = FindAnyObjectByType<PlayerRobotsDataManager>(FindObjectsInactive.Include);
	}

	private void OnBonusCollected(GameObject sender)
	{
		GiveEffect(sender);
		AddPointsToPlayerIfPossible(sender);
		PlaySound();
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

	private void PlaySound()
	{
		if(stageSoundManager != null)
		{
			stageSoundManager.PlaySound(SoundEffectType.BonusCollect);
		}
	}
}