using UnityEngine;

[CreateAssetMenu(menuName = "Game/Player Data")]
public class PlayerData : ScriptableObject
{
	public int Score
	{
		get
		{
			return score;
		}
		set
		{
			score = Mathf.Clamp(value, 0, int.MaxValue);
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

	private int score, lives, rank;

	public void ResetData()
	{
		score = initialScore;
		lives = initialLives;
		rank = initialRank;
	}
}