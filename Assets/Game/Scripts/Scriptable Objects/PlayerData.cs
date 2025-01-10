using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Game/Player Data")]
public class PlayerData : RobotData<PlayerRobotRank>
{
	public Dictionary<EnemyData, int> DefeatedEnemies {get; private set;} = new Dictionary<EnemyData, int>();
	public PlayerRobotEntitySpawner Spawner {get; set;}
	
	public int Score
	{
		get => score;
		set => score = Mathf.Clamp(value, 0, int.MaxValue);
	}

	public int Lives
	{
		get => lives;
		set => lives = Mathf.Clamp(value, 0, maxLives);
	}

	public int BonusLifeThreshold
	{
		get => bonusLifeThreshold;
		set => bonusLifeThreshold = Mathf.Clamp(value, 0, int.MaxValue);
	}
	
	[SerializeField, Min(0)] private int initialLives = 2;
	[SerializeField, Min(0)] private int maxLives = 9;
	[SerializeField, Min(1)] private int initialBonusLifeThreshold = 20000;
	[SerializeField, Min(1)] private int initialRankNumber = 1;

	private int score;
	private int lives;
	private int bonusLifeThreshold;

	public void ResetData()
	{
		score = 0;
		lives = initialLives;
		bonusLifeThreshold = initialBonusLifeThreshold;

		ResetRank();
		ResetDefeatedEnemies();
	}

	public void ResetRank()
	{
		RankNumber = initialRankNumber;
	}
	
	public void ResetDefeatedEnemies()
	{
		DefeatedEnemies.Clear();
	}

	public void AddDefeatedEnemy(EnemyData enemyData)
	{
		if(DefeatedEnemies.ContainsKey(enemyData))
		{
			++DefeatedEnemies[enemyData];
		}
		else
		{
			DefeatedEnemies.Add(enemyData, 1);
		}
	}

	public void IncreaseBonusLifeThreshold()
	{
		BonusLifeThreshold += initialBonusLifeThreshold;
	}
}