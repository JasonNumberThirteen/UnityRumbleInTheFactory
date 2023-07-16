using UnityEngine;

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

	public void ResetData()
	{
		score = initialScore;
		lives = initialLives;
		rank = initialRank;
		bonusLifeThreshold = initialBonusLifeThreshold;
	}

	private void CheckBonusLifeThreshold()
	{
		if(score >= bonusLifeThreshold)
		{
			++Lives;
			bonusLifeThreshold += initialBonusLifeThreshold;
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