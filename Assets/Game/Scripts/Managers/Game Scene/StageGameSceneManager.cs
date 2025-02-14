using UnityEngine;

[RequireComponent(typeof(Timer))]
public class StageGameSceneManager : GameSceneManager
{
	[SerializeField] private float delayOnWonStage = 3f;
	[SerializeField] private float delayOnGameOver = 2f;
	
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
				gameOverTextUI.textReachedTargetPositionEvent.AddListener(OnTextReachedTargetPosition);
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
				gameOverTextUI.textReachedTargetPositionEvent.RemoveListener(OnTextReachedTargetPosition);
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

	private void OnTextReachedTargetPosition()
	{
		timer.StartTimerWithSetDuration(delayOnGameOver);
	}

	private void OnStageStateChanged(StageState stageState)
	{
		switch (stageState)
		{
			case StageState.Interrupted:
				timer.InterruptTimerIfPossible();
				break;
			
			case StageState.Won:
				timer.StartTimerWithSetDuration(delayOnWonStage);
				break;
		}
	}
}