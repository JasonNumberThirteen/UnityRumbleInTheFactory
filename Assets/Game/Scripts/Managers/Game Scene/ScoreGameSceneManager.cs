using UnityEngine;

[RequireComponent(typeof(Timer))]
public class ScoreGameSceneManager : GameSceneManager
{
	[SerializeField] private GameData gameData;

	private Timer timer;
	private ScoreEnemyTypeSwitchManager scoreEnemyTypeSwitchManager;

	private void Awake()
	{
		timer = GetComponent<Timer>();
		scoreEnemyTypeSwitchManager = ObjectMethods.FindComponentOfType<ScoreEnemyTypeSwitchManager>();

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

			if(scoreEnemyTypeSwitchManager != null)
			{
				scoreEnemyTypeSwitchManager.lastEnemyTypeReachedEvent.AddListener(timer.StartTimer);
			}
		}
		else
		{
			timer.timerFinishedEvent.RemoveListener(OnTimerFinished);

			if(scoreEnemyTypeSwitchManager != null)
			{
				scoreEnemyTypeSwitchManager.lastEnemyTypeReachedEvent.RemoveListener(timer.StartTimer);
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