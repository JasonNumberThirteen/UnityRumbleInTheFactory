using UnityEngine;

public class EnemyRobotEntityHealth : RobotEntityHealth
{
	[SerializeField] private EnemyData enemyData;

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
		if(enemyData == null || sender == null || !sender.TryGetComponent(out PlayerRobot playerRobot))
		{
			return;
		}

		var playerData = playerRobot.GetPlayerData();

		if(playerData != null)
		{
			playerData.AddDefeatedEnemy(enemyData);

			if(playersDataManager != null)
			{
				playersDataManager.ModifyScore(playerData, enemyData.GetPointsForDefeat(), gameObject);
			}
		}
	}

	private void PlaySound()
	{
		if(stageSoundManager != null)
		{
			stageSoundManager.PlaySound(SoundEffectType.EnemyRobotExplosion);
		}
	}
}