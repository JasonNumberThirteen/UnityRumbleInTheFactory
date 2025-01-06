using UnityEngine;
using UnityEngine.Events;

public class PlayersDataManager : MonoBehaviour
{
	public UnityEvent<int, GameObject> playerScoreChangedEvent;
	public UnityEvent playerLivesChangedEvent;
	public UnityEvent playerRankChangedEvent;
	
	[SerializeField] private GameData gameData;
	[SerializeField] private PlayersListData playersListData;

	public void CheckPlayersLives()
	{
		if(playersListData != null && !playersListData.Any(playerData => playerData.Spawner != null && playerData.Lives > 0))
		{
			StageManager.instance.SetGameAsOver();
		}
	}

	public void ModifyScore(PlayerData playerData, int score, GameObject go)
	{
		if(playerData == null)
		{
			return;
		}

		var previousScore = playerData.Score;

		playerData.Score += score;

		if(playerData.Score != previousScore)
		{
			AddLifeIfPossible(playerData);

			if(gameData != null)
			{
				gameData.SetHighScoreIfPossible(score, () => ModifyLives(playerData, 1));
			}
			
			playerScoreChangedEvent?.Invoke(score, go);
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

		var previousRank = playerData.RankNumber;

		playerData.RankNumber += ranks;

		if(playerData.RankNumber != previousRank)
		{
			playerRankChangedEvent?.Invoke();
		}
	}

	private void Awake()
	{
		if(playersListData != null)
		{
			playersListData.ForEach(playerData => playerData.ResetDefeatedEnemies());
		}
	}

	private void AddLifeIfPossible(PlayerData playerData)
	{
		if(playerData == null || playerData.Score < playerData.BonusLifeThreshold)
		{
			return;
		}
		
		ModifyLives(playerData, 1);
		playerData.IncreaseBonusLifeThreshold();
		AddLifeIfPossible(playerData);
	}
}