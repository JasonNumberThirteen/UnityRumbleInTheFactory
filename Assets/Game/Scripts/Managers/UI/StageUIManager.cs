using UnityEngine;

public class StageUIManager : MonoBehaviour
{
	[SerializeField] private GainedPointsCounterTextUI gainedPointsCounterTextUIPrefab;

	private PlayersDataManager playersDataManager;
	private StageStateManager stageStateManager;
	private StageSceneFlowManager stageSceneFlowManager;
	private TranslationBackgroundPanelUI translationBackgroundPanelUI;
	private MainCanvasUI mainCanvasUI;
	private PauseTextUI pauseTextUI;

	private void Awake()
	{
		playersDataManager = FindFirstObjectByType<PlayersDataManager>(FindObjectsInactive.Include);
		stageStateManager = FindFirstObjectByType<StageStateManager>(FindObjectsInactive.Include);
		stageSceneFlowManager = FindFirstObjectByType<StageSceneFlowManager>(FindObjectsInactive.Include);
		translationBackgroundPanelUI = FindFirstObjectByType<TranslationBackgroundPanelUI>(FindObjectsInactive.Include);
		mainCanvasUI = FindAnyObjectByType<MainCanvasUI>(FindObjectsInactive.Include);
		pauseTextUI = FindFirstObjectByType<PauseTextUI>(FindObjectsInactive.Include);

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
			if(playersDataManager != null)
			{
				playersDataManager.playerScoreChangedEvent.AddListener(OnPlayerScoreChanged);
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
			if(playersDataManager != null)
			{
				playersDataManager.playerScoreChangedEvent.RemoveListener(OnPlayerScoreChanged);
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
		if(gainedPointsCounterTextUIPrefab == null)
		{
			return;
		}
		
		var parent = mainCanvasUI != null ? mainCanvasUI.transform : null;
		var instance = Instantiate(gainedPointsCounterTextUIPrefab, parent);

		if(instance != null)
		{
			instance.Setup(points, position*16);
		}
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