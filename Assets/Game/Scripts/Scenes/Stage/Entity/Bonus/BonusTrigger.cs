using UnityEngine;

public abstract class BonusTrigger : MonoBehaviour, ITriggerableOnEnter
{
	protected StageSoundManager stageSoundManager;

	[SerializeField, Min(0)] private int points = 500;

	private StageStateManager stageStateManager;
	private PlayersDataManager playersDataManager;
	
	public virtual void TriggerOnEnter(GameObject sender)
	{
		OnBonusCollected(sender);
		Destroy(gameObject);
	}

	private void Awake()
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
		if(playersDataManager != null && sender.TryGetComponent(out PlayerRobotData playerRobotData) && playerRobotData.Data != null)
		{
			playersDataManager.ModifyScore(playerRobotData.Data, points);
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