using UnityEngine;

public abstract class BonusTriggerEventsReceiver : MonoBehaviour, ITriggerableOnEnter
{
	protected StageSoundManager stageSoundManager;

	[SerializeField, Min(0)] private int points = 500;

	protected PlayersDataManager playersDataManager;

	private StageStateManager stageStateManager;
	
	public virtual void TriggerOnEnter(GameObject sender)
	{
		OnBonusCollected(sender);
		Destroy(gameObject);
	}

	protected virtual void Awake()
	{
		stageSoundManager = FindAnyObjectByType<StageSoundManager>(FindObjectsInactive.Include);
		stageStateManager = FindAnyObjectByType<StageStateManager>(FindObjectsInactive.Include);
		playersDataManager = FindAnyObjectByType<PlayersDataManager>(FindObjectsInactive.Include);
	}

	private void OnBonusCollected(GameObject sender)
	{
		if(stageStateManager == null || stageStateManager.GameIsOver())
		{
			return;
		}
		
		AddPointsToPlayerIfPossible(sender);
		PlaySound();
	}

	private void AddPointsToPlayerIfPossible(GameObject sender)
	{
		if(!sender.TryGetComponent(out PlayerRobot playerRobot))
		{
			return;
		}
		
		var playerData = playerRobot.GetPlayerData();

		if(playersDataManager != null && playerData != null)
		{
			playersDataManager.ModifyScore(playerData, points, gameObject);
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