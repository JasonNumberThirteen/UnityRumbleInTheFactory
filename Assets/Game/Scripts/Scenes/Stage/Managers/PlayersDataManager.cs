using UnityEngine;
using UnityEngine.Events;

public class PlayersDataManager : MonoBehaviour
{
	public UnityEvent playerScoreChangedEvent;
	public UnityEvent playerLivesChangedEvent;
	public UnityEvent playerRankChangedEvent;
	
	[SerializeField] private PlayersListData playersListData;

	public void ModifyScore(PlayerData playerData, int score)
	{
		if(playerData == null)
		{
			return;
		}

		var previousScore = playerData.Score;

		playerData.Score += score;

		if(playerData.Score != previousScore)
		{
			playerScoreChangedEvent?.Invoke();
		}
	}

	public void ModifyLives(PlayerData playerData, int lives)
	{
		if(playerData == null)
		{
			return;
		}

		var previousLives = playerData.Lives;

		playerData.Lives += lives;

		if(playerData.Lives != previousLives)
		{
			playerLivesChangedEvent?.Invoke();
		}
	}

	public void ModifyRank(PlayerData playerData, int ranks)
	{
		if(playerData == null)
		{
			return;
		}

		var previousRank = playerData.Rank;

		playerData.Rank += ranks;

		if(playerData.Rank != previousRank)
		{
			playerRankChangedEvent?.Invoke();
		}
	}
}