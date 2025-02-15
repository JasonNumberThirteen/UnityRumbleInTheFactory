using UnityEngine;

public class StageUIManager : UIManager
{
	[SerializeField] private GainedPointsIntCounterTextUIParent gainedPointsIntCounterTextUIParentPrefab;

	private PlayerRobotsDataManager playerRobotsDataManager;
	private StageStateManager stageStateManager;
	private StageSceneFlowManager stageSceneFlowManager;
	private TranslationBackgroundPanelUI translationBackgroundPanelUI;
	private MainCanvasUI mainCanvasUI;
	private PauseTextUI pauseTextUI;

	private void Awake()
	{
		playerRobotsDataManager = ObjectMethods.FindComponentOfType<PlayerRobotsDataManager>();
		stageStateManager = ObjectMethods.FindComponentOfType<StageStateManager>();
		stageSceneFlowManager = ObjectMethods.FindComponentOfType<StageSceneFlowManager>();
		translationBackgroundPanelUI = ObjectMethods.FindComponentOfType<TranslationBackgroundPanelUI>();
		mainCanvasUI = ObjectMethods.FindComponentOfType<MainCanvasUI>();
		pauseTextUI = ObjectMethods.FindComponentOfType<PauseTextUI>();

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
			if(playerRobotsDataManager != null)
			{
				playerRobotsDataManager.playerScoreChangedEvent.AddListener(OnPlayerScoreChanged);
			}

			if(stageSceneFlowManager != null)
			{
				stageSceneFlowManager.stageStartedEvent.AddListener(OnStageStarted);
				stageSceneFlowManager.stageActivatedEvent.AddListener(OnStageActivated);
			}

			if(stageStateManager != null)
			{
				stageStateManager.stageStateChangedEvent.AddListener(OnStageStateChanged);
			}
		}
		else
		{
			if(playerRobotsDataManager != null)
			{
				playerRobotsDataManager.playerScoreChangedEvent.RemoveListener(OnPlayerScoreChanged);
			}

			if(stageSceneFlowManager != null)
			{
				stageSceneFlowManager.stageStartedEvent.RemoveListener(OnStageStarted);
				stageSceneFlowManager.stageActivatedEvent.RemoveListener(OnStageActivated);
			}

			if(stageStateManager != null)
			{
				stageStateManager.stageStateChangedEvent.RemoveListener(OnStageStateChanged);
			}
		}
	}

	private void OnPlayerScoreChanged(int score, GameObject go)
	{
		SpawnGainedPointsCounterIfPossible(score, go.transform.position);
	}

	private void SpawnGainedPointsCounterIfPossible(int points, Vector2 position)
	{
		if(gainedPointsIntCounterTextUIParentPrefab == null)
		{
			return;
		}
		
		var parent = mainCanvasUI != null ? mainCanvasUI.transform : null;
		
		Instantiate(gainedPointsIntCounterTextUIParentPrefab, parent).Setup(points, position*16);
	}

	private void OnStageStarted()
	{
		if(translationBackgroundPanelUI != null)
		{
			translationBackgroundPanelUI.StartTranslation();
		}
	}

	private void OnStageActivated()
	{
		if(translationBackgroundPanelUI != null)
		{
			translationBackgroundPanelUI.gameObject.SetActive(false);
		}
	}

	private void OnStageStateChanged(StageState stageState)
	{
		if(pauseTextUI != null)
		{
			pauseTextUI.SetActive(stageStateManager != null && stageStateManager.StateIsSetTo(StageState.Paused));
		}
	}
}