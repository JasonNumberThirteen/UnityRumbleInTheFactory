using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Game/Player Data")]
public class PlayerData : ScriptableObject
{
	public GameData gameData;

	public Dictionary<EnemyData, int> DefeatedEnemies {get; private set;}
	
	public int Score
	{
		get
		{
			return score;
		}
		set
		{
			score = Mathf.Clamp(value, 0, int.MaxValue);

			CheckBonusLifeThreshold();
			CheckHighScore();
		}
	}

	public int Lives
	{
		get
		{
			return lives;
		}
		set
		{
			lives = Mathf.Clamp(value, 0, 9);

			StageManager.instance.uiManager.UpdateCounters();
		}
	}

	public int Rank
	{
		get
		{
			return rank;
		}
		set
		{
			rank = Mathf.Clamp(value, 1, 4);
		}
	}
	
	[SerializeField][Min(0)] private int initialScore;
	[SerializeField][Min(0)] private int initialLives = 2;
	[SerializeField][Min(1)] private int initialRank = 1;
	[SerializeField][Min(1)] private int initialBonusLifeThreshold = 20000;

	private int score, lives, rank, bonusLifeThreshold;

	public void ResetData()
	{
		score = initialScore;
		lives = initialLives;
		rank = initialRank;
		bonusLifeThreshold = initialBonusLifeThreshold;
		DefeatedEnemies = new Dictionary<EnemyData, int>();
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

	private void CheckBonusLifeThreshold()
	{
		if(score >= bonusLifeThreshold)
		{
			++Lives;
			bonusLifeThreshold += initialBonusLifeThreshold;
			
			CheckBonusLifeThreshold();
		}
	}

	private void CheckHighScore()
	{
		if(score >= gameData.highScore && !gameData.beatenHighScore)
		{
			gameData.beatenHighScore = true;
			++Lives;
		}
	}
}