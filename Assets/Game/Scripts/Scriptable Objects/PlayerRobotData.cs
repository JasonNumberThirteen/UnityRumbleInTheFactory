using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Game/Player Robot Data")]
public class PlayerRobotData : RobotData
{
	public SaveablePlayerData saveablePlayerData;
	
	public Dictionary<EnemyRobotData, int> DefeatedEnemies {get; private set;} = new Dictionary<EnemyRobotData, int>();
	public PlayerRobotEntitySpawner Spawner {get; set;}

	public int CurrentHealth {get; set;}
	
	public int Score
	{
		get => saveablePlayerData.score;
		set => saveablePlayerData.score = Mathf.Clamp(value, 0, int.MaxValue);
	}

	public int Lives
	{
		get => lives;
		set => lives = Mathf.Clamp(value, 0, maxLives);
	}

	public int RankNumber
	{
		get => rankNumber;
		set => rankNumber = Mathf.Clamp(value, 1, GetNumberOfRanks());
	}
	
	[SerializeField] private PlayerRobotRank[] ranks;
	[SerializeField, Min(0)] private int initialLives = 2;
	[SerializeField, Min(0)] private int maxLives = 9;
	[SerializeField, Min(1)] private int initialRankNumber = 1;

	private int lives;
	private int rankNumber;

	public override RobotRank GetRankByIndex(int index) => index >= 0 && index < GetNumberOfRanks() ? ranks[index] : null;
	public override int GetNumberOfRanks() => ranks != null && ranks.Length > 0 ? ranks.Length : 1;

	public void ResetData()
	{
		lives = initialLives;
		
		saveablePlayerData.ResetData();
		ResetRank();
		ResetDefeatedEnemies();
		ResetCurrentHealth();
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

	private void ResetCurrentHealth()
	{
		var rank = GetRankByIndex(rankNumber - 1);

		if(rank != null)
		{
			CurrentHealth = rank.GetHealth();
		}
	}
}