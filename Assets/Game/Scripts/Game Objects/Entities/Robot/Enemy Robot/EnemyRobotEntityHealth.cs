using UnityEngine;

public class EnemyRobotEntityHealth : RobotEntityHealth
{
	[SerializeField] private EnemyRobotData enemyRobotData;

	private StageStateManager stageStateManager;
	private PlayerRobotsDataManager playerRobotsDataManager;

	public void IncreaseHealthBy(int health)
	{
		CurrentHealth += health;
	}

	protected override void Awake()
	{
		base.Awake();

		stageStateManager = ObjectMethods.FindComponentOfType<StageStateManager>();
		playerRobotsDataManager = ObjectMethods.FindComponentOfType<PlayerRobotsDataManager>();
	}

	protected override void Die(GameObject sender)
	{
		OnDeath(sender);
		base.Die(sender);
	}

	private void OnDeath(GameObject sender)
	{
		if(stageStateManager != null && !stageStateManager.GameIsOver())
		{
			ModifyPlayerRobotDataIfPossible(sender);
		}
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

			if(playerRobotsDataManager != null)
			{
				playerRobotsDataManager.ModifyScore(playerRobotData, enemyRobotData.GetPointsForDefeat(), gameObject);
			}
		}
	}
}