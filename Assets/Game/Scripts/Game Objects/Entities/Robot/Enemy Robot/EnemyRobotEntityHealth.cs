using UnityEngine;

public class EnemyRobotEntityHealth : RobotEntityHealth
{
	[SerializeField] private EnemyRobotData enemyRobotData;

	private StageStateManager stageStateManager;
	private PlayersDataManager playersDataManager;

	public void IncreaseHealthBy(int health)
	{
		CurrentHealth += health;
	}

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

		ModifyPlayerRobotDataIfPossible(sender);
		PlaySound();
	}

	private void ModifyPlayerRobotDataIfPossible(GameObject sender)
	{
		if(enemyRobotData == null || sender == null || !sender.TryGetComponent(out PlayerRobotEntity playerRobotEntity))
		{
			return;
		}

		var playerRobotData = playerRobotEntity.GetPlayerRobotData();

		if(playerRobotData != null)
		{
			playerRobotData.AddDefeatedEnemy(enemyRobotData);

			if(playersDataManager != null)
			{
				playersDataManager.ModifyScore(playerRobotData, enemyRobotData.GetPointsForDefeat(), gameObject);
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