using UnityEngine;
using UnityEngine.Events;

public class PlayerRobotsDataManager : MonoBehaviour
{
	public UnityEvent<int, GameObject> playerScoreChangedEvent;
	public UnityEvent playerLivesChangedEvent;
	
	[SerializeField] private GameData gameData;
	[SerializeField] private PlayerRobotsListData playerRobotsListData;

	private StageSceneFlowManager stageSceneFlowManager;

	public void CheckPlayersLives()
	{
		if(stageSceneFlowManager != null && playerRobotsListData != null && !playerRobotsListData.Any(playerData => playerData.Spawner != null && playerData.Lives > 0))
		{
			stageSceneFlowManager.SetGameAsOver();
		}
	}

	public void ModifyScore(PlayerRobotData playerRobotData, int score, GameObject go)
	{
		if(playerRobotData == null)
		{
			return;
		}

		var previousScore = playerRobotData.Score;

		playerRobotData.Score += score;

		if(playerRobotData.Score != previousScore)
		{
			AddLifeIfPossible(playerRobotData);

			if(gameData != null)
			{
				gameData.SetHighScoreIfPossible(playerRobotData.Score, () => ModifyLives(playerRobotData, 1));
			}
			
			playerScoreChangedEvent?.Invoke(score, go);
		}
	}

	public void ModifyLives(PlayerRobotData playerRobotData, int lives)
	{
		if(playerRobotData == null)
		{
			return;
		}

		var previousLives = playerRobotData.Lives;

		playerRobotData.Lives += lives;

		if(playerRobotData.Lives != previousLives)
		{
			playerLivesChangedEvent?.Invoke();
		}
	}

	private void Awake()
	{
		stageSceneFlowManager = FindAnyObjectByType<StageSceneFlowManager>(FindObjectsInactive.Include);
		
		if(playerRobotsListData != null)
		{
			playerRobotsListData.ForEach(playerRobotData => playerRobotData.ResetDefeatedEnemies());
		}
	}

	private void AddLifeIfPossible(PlayerRobotData playerRobotData)
	{
		if(playerRobotData == null || playerRobotData.Score < playerRobotData.BonusLifeThreshold)
		{
			return;
		}
		
		ModifyLives(playerRobotData, 1);
		playerRobotData.IncreaseBonusLifeThreshold();
		AddLifeIfPossible(playerRobotData);
	}
}