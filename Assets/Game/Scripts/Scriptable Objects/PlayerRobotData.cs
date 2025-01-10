using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Game/Player Robot Data")]
public class PlayerRobotData : RobotData
{
	public Dictionary<EnemyRobotData, int> DefeatedEnemies {get; private set;} = new Dictionary<EnemyRobotData, int>();
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

	public override int RankNumber
	{
		get => rankNumber;
		set => rankNumber = Mathf.Clamp(value, 1, ranks != null && ranks.Length > 0 ? ranks.Length : 1);
	}
	
	[SerializeField] private PlayerRobotRank[] ranks;
	[SerializeField, Min(0)] private int initialLives = 2;
	[SerializeField, Min(0)] private int maxLives = 9;
	[SerializeField, Min(1)] private int initialBonusLifeThreshold = 20000;
	[SerializeField, Min(1)] private int initialRankNumber = 1;

	private int score;
	private int lives;
	private int bonusLifeThreshold;
	private int rankNumber;

	public override RobotRank GetRank() => ranks[RankNumber - 1];

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

	public void AddDefeatedEnemy(EnemyRobotData enemyRobotData)
	{
		if(DefeatedEnemies.ContainsKey(enemyRobotData))
		{
			++DefeatedEnemies[enemyRobotData];
		}
		else
		{
			DefeatedEnemies.Add(enemyRobotData, 1);
		}
	}

	public void IncreaseBonusLifeThreshold()
	{
		BonusLifeThreshold += initialBonusLifeThreshold;
	}
}