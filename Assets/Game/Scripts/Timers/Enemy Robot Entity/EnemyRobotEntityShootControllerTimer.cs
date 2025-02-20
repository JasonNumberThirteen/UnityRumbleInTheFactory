using UnityEngine;

public class EnemyRobotEntityShootControllerTimer : Timer
{
	[SerializeField] private GameData gameData;

	private RobotEntityRankController robotEntityRankController;
	
	private void Awake()
	{
		robotEntityRankController = GetComponentInParent<RobotEntityRankController>();

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
			if(robotEntityRankController != null)
			{
				robotEntityRankController.rankChangedEvent.AddListener(OnRankChanged);
			}
		}
		else
		{
			if(robotEntityRankController != null)
			{
				robotEntityRankController.rankChangedEvent.RemoveListener(OnRankChanged);
			}
		}
	}

	private void OnRankChanged(RobotRank robotRank, bool setOnStart)
	{
		var baseShootDelayValue = robotRank != null && robotRank is EnemyRobotRank enemyRobotRank ? enemyRobotRank.GetShootDelay() : 0f;
		var shootDelayMultiplier = GameDataMethods.GetDifficultyTierValue(gameData, tier => tier.GetEnemyShootDelayMultiplier());
		
		SetDuration(baseShootDelayValue*shootDelayMultiplier);
	}
}