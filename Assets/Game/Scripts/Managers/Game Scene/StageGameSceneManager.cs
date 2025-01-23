using UnityEngine;

[RequireComponent(typeof(Timer))]
public class StageGameSceneManager : GameSceneManager
{
	private Timer timer;
	private GameOverTextUI gameOverTextUI;
	private StageStateManager stageStateManager;

	private void Awake()
	{
		timer = GetComponent<Timer>();
		gameOverTextUI = ObjectMethods.FindComponentOfType<GameOverTextUI>();
		stageStateManager = ObjectMethods.FindComponentOfType<StageStateManager>();

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

			if(gameOverTextUI != null)
			{
				gameOverTextUI.textReachedTargetPositionEvent.AddListener(OnGameOverTextUIReachedTargetPosition);
			}

			if(stageStateManager != null)
			{
				stageStateManager.stageStateChangedEvent.AddListener(OnStageStateChanged);
			}
		}
		else
		{
			timer.timerFinishedEvent.RemoveListener(OnTimerFinished);

			if(gameOverTextUI != null)
			{
				gameOverTextUI.textReachedTargetPositionEvent.RemoveListener(OnGameOverTextUIReachedTargetPosition);
			}

			if(stageStateManager != null)
			{
				stageStateManager.stageStateChangedEvent.RemoveListener(OnStageStateChanged);
			}
		}
	}

	private void OnTimerFinished()
	{
		LoadSceneByName(SCORE_SCENE_NAME);
	}

	private void OnGameOverTextUIReachedTargetPosition()
	{
		timer.StartTimer();
	}

	private void OnStageStateChanged(StageState stageState)
	{
		if(stageState == StageState.Interrupted)
		{
			timer.InterruptTimerIfPossible();
		}
		else if(stageState == StageState.Won)
		{
			timer.StartTimer();
		}
	}
}