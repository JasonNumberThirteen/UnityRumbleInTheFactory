using UnityEngine;

[RequireComponent(typeof(Timer))]
public class ScoreGameSceneManager : GameSceneManager
{
	[SerializeField] private GameData gameData;

	private Timer timer;
	private ScoreEnemyRobotTypeSwitchManager scoreEnemyRobotTypeSwitchManager;

	private void Awake()
	{
		timer = GetComponent<Timer>();
		scoreEnemyRobotTypeSwitchManager = ObjectMethods.FindComponentOfType<ScoreEnemyRobotTypeSwitchManager>();

		RegisterToListeners(true);
	}

	private void OnDestroy()
	{
		RegisterToListeners(false);
	}

	private void RegisterToListeners(bool register)
	{
		if(register)
		{
			timer.timerFinishedEvent.AddListener(OnTimerFinished);

			if(scoreEnemyRobotTypeSwitchManager != null)
			{
				scoreEnemyRobotTypeSwitchManager.lastEnemyRobotTypeReachedEvent.AddListener(timer.StartTimer);
			}
		}
		else
		{
			timer.timerFinishedEvent.RemoveListener(OnTimerFinished);

			if(scoreEnemyRobotTypeSwitchManager != null)
			{
				scoreEnemyRobotTypeSwitchManager.lastEnemyRobotTypeReachedEvent.RemoveListener(timer.StartTimer);
			}
		}
	}

	private void OnTimerFinished()
	{
		var gameIsOver = gameData != null && gameData.GameIsOver;
		
		if(!gameIsOver && gameData != null)
		{
			gameData.IncreaseDifficultyIfNeeded();
			gameData.AdvanceToNextStage();
		}

		LoadSceneByName(gameIsOver ? GAME_OVER_SCENE_NAME : STAGE_SCENE_NAME);
	}
}