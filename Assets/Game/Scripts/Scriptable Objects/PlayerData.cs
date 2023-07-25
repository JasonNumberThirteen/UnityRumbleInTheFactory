using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Game/Player Data")]
public class PlayerData : ScriptableObject
{
	public GameData gameData;
	
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
	
	[SerializeField] private int initialScore;
	[SerializeField] private int initialLives = 2;
	[SerializeField] private int initialRank = 1;
	[SerializeField] private int initialBonusLifeThreshold = 20000;

	private int score, lives, rank, bonusLifeThreshold;
	private Dictionary<EnemyData, int> defeatedEnemies;

	public void ResetData()
	{
		score = initialScore;
		lives = initialLives;
		rank = initialRank;
		bonusLifeThreshold = initialBonusLifeThreshold;
		defeatedEnemies = new Dictionary<EnemyData, int>();
	}

	public void AddDefeatedEnemy(EnemyData enemyData)
	{
		if(defeatedEnemies.ContainsKey(enemyData))
		{
			++defeatedEnemies[enemyData];
		}
		else
		{
			defeatedEnemies.Add(enemyData, 1);
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