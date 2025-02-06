using UnityEngine;
using UnityEngine.Events;

public class PlayerRobotsDataManager : MonoBehaviour
{
	public UnityEvent<int, GameObject> playerScoreChangedEvent;
	public UnityEvent<int, int> playerLivesChangedEvent;
	
	[SerializeField] private GameData gameData;
	[SerializeField] private PlayerRobotsListData playerRobotsListData;

	private StageSceneFlowManager stageSceneFlowManager;

	public void CheckPlayersLives()
	{
		if(stageSceneFlowManager != null && playerRobotsListData != null && !playerRobotsListData.Any(playerData => playerData.Spawner != null && playerData.Lives > 0) && ObjectMethods.FindComponentsOfType<PlayerRobotEntity>().Length == 0)
		{
			stageSceneFlowManager.SetGameAsOverIfNeeded();
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
			if(gameData != null)
			{
				gameData.SetHighScoreIfPossible(playerRobotData.Score, () => ModifyLives(playerRobotData, 1));
			}
			
			playerScoreChangedEvent?.Invoke(score, go);
		}
	}

	public void ModifyLives(PlayerRobotData playerRobotData, int lives)
	{
		if(playerRobotData == null || lives == 0)
		{
			return;
		}

		playerRobotData.Lives += lives;

		playerLivesChangedEvent?.Invoke(playerRobotData.Lives, lives);
	}

	private void Awake()
	{
		stageSceneFlowManager = ObjectMethods.FindComponentOfType<StageSceneFlowManager>();
		
		if(playerRobotsListData != null)
		{
			playerRobotsListData.ForEach(playerRobotData => playerRobotData.ResetDefeatedEnemies());
		}
	}
}