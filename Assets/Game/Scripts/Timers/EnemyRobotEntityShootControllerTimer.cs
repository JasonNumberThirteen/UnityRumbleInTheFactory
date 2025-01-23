using UnityEngine;

public class EnemyRobotEntityShootControllerTimer : Timer
{
	[SerializeField] private GameData gameData;
	
	private void Awake()
	{
		if(gameData != null)
		{
			SetDuration(gameData.GetDifficultyTierValue(tier => tier.GetEnemyShootDelay()));
		}
	}
}