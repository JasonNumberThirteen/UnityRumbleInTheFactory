using UnityEngine;

[RequireComponent(typeof(Timer))]
public class ScoreSceneManager : GameSceneManager
{
	[SerializeField] private GameData gameData;

	private Timer timer;

	private void Awake()
	{
		timer = GetComponent<Timer>();

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
			timer.onEnd.AddListener(OnTimerEnd);
		}
		else
		{
			timer.onEnd.RemoveListener(OnTimerEnd);
		}
	}

	private void OnTimerEnd()
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