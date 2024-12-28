using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Game/Player Data")]
public class PlayerData : MainMenuData
{
	public Dictionary<EnemyData, int> DefeatedEnemies {get; private set;} = new Dictionary<EnemyData, int>();
	public PlayerSpawner Spawner {get; set;}
	
	public int Score
	{
		get => score;
		set
		{
			score = Mathf.Clamp(value, 0, int.MaxValue);
		}
	}

	public int Lives
	{
		get => lives;
		set
		{
			lives = Mathf.Clamp(value, 0, maxLives);
		}
	}

	public int BonusLifeThreshold
	{
		get => bonusLifeThreshold;
		set => Mathf.Clamp(value, 0, int.MaxValue);
	}

	public int Rank
	{
		get => currentRank;
		set
		{
			currentRank = Mathf.Clamp(value, 1, 4);
		}
	}
	
	[SerializeField] private GameData gameData;
	[SerializeField, Min(0)] private int initialLives = 2;
	[SerializeField, Min(1)] private int initialRank = 1;
	[SerializeField, Min(1)] private int initialBonusLifeThreshold = 20000;
	[SerializeField, Min(0)] private int maxLives = 9;

	private int score;
	private int lives;
	private int bonusLifeThreshold;
	private int currentRank;
	
	public override int GetMainMenuCounterValue() => score;

	public void ResetData()
	{
		score = 0;
		lives = initialLives;
		bonusLifeThreshold = initialBonusLifeThreshold;

		ResetCurrentRank();
		ResetDefeatedEnemies();
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

	public void ResetCurrentRank()
	{
		currentRank = initialRank;
	}
}