using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PlayerRobotsDataManager : MonoBehaviour
{
	public UnityEvent<int, GameObject> playerScoreWasChangedEvent;
	public UnityEvent<int, int> playerLivesWereChangedEvent;
	
	[SerializeField] private GameData gameData;
	[SerializeField] private PlayerRobotsListData playerRobotsListData;

	private PlayerRobotEntitiesTracker playerRobotsEntitiesTracker;

	public bool AllPlayersLostAllLives() => playerRobotsListData != null && playerRobotsEntitiesTracker != null && !playerRobotsListData.GetPlayerRobotsData().Any(playerData => playerData.Spawner != null && playerData.IsAlive) && playerRobotsEntitiesTracker.GetPlayerRobotEntities().Count == 0;

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
			if(GameDataMethods.GameDataIsDefined(gameData))
			{
				gameData.SetHighScoreIfPossible(playerRobotData.Score, () => ModifyLives(playerRobotData, 1));
			}
			
			playerScoreWasChangedEvent?.Invoke(score, go);
		}
	}

	public void ModifyLives(PlayerRobotData playerRobotData, int lives)
	{
		if(playerRobotData == null || lives == 0)
		{
			return;
		}

		playerRobotData.Lives += lives;

		playerLivesWereChangedEvent?.Invoke(playerRobotData.Lives, lives);
	}

	private void Awake()
	{
		playerRobotsEntitiesTracker = ObjectMethods.FindComponentOfType<PlayerRobotEntitiesTracker>();
		
		if(playerRobotsListData != null)
		{
			playerRobotsListData.GetPlayerRobotsData().ForEach(playerRobotData =>
			{
				playerRobotData.ResetDefeatedEnemies();

				playerRobotData.WasAliveOnCurrentStage = playerRobotData.IsAlive;
			});
		}
	}
}