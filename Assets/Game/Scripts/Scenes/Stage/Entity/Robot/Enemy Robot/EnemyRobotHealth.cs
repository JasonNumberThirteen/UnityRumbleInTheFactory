using UnityEngine;

public class EnemyRobotHealth : RobotHealth
{
	public EnemyData data;

	private StageStateManager stageStateManager;
	private PlayersDataManager playersDataManager;

	protected override void Awake()
	{
		base.Awake();

		stageStateManager = FindAnyObjectByType<StageStateManager>();
		playersDataManager = FindAnyObjectByType<PlayersDataManager>(FindObjectsInactive.Include);
	}

	protected override void Die(GameObject sender)
	{
		OnDeath(sender);
		base.Die(sender);
	}

	private void OnDeath(GameObject sender)
	{
		if(stageStateManager == null || stageStateManager.GameIsOver())
		{
			return;
		}

		ModifyPlayerDataIfPossible(sender);
		PlaySound();
	}

	private void ModifyPlayerDataIfPossible(GameObject sender)
	{
		if(playersDataManager == null || !sender.TryGetComponent(out PlayerRobotData playerRobotData) && playerRobotData.Data == null)
		{
			return;
		}
		
		playerRobotData.Data.AddDefeatedEnemy(data);
		playersDataManager.ModifyScore(playerRobotData.Data, data.GetPointsForDefeat(), gameObject);
	}

	private void PlaySound()
	{
		if(stageSoundManager != null)
		{
			stageSoundManager.PlaySound(SoundEffectType.EnemyRobotExplosion);
		}
	}
}