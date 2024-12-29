using UnityEngine;

[RequireComponent(typeof(Timer))]
public class StageGameSceneManager : GameSceneManager
{
	private Timer timer;
	private GameOverTextUI gameOverTextUI;

	private void Awake()
	{
		timer = GetComponent<Timer>();
		gameOverTextUI = FindAnyObjectByType<GameOverTextUI>();

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

			if(gameOverTextUI != null)
			{
				gameOverTextUI.textReachedTargetPositionEvent.AddListener(OnGameOverTextUIReachedTargetPosition);
			}
		}
		else
		{
			timer.onEnd.RemoveListener(OnTimerEnd);

			if(gameOverTextUI != null)
			{
				gameOverTextUI.textReachedTargetPositionEvent.RemoveListener(OnGameOverTextUIReachedTargetPosition);
			}
		}
	}

	private void OnTimerEnd()
	{
		LoadSceneByName(SCORE_SCENE_NAME);
	}

	private void OnGameOverTextUIReachedTargetPosition()
	{
		timer.StartTimer();
	}
}