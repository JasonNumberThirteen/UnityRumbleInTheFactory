using UnityEngine;

public class StageUIManager : MonoBehaviour
{
	[SerializeField] private GameObject canvasGO;
	[SerializeField] private GainedPointsCounterTextUI gainedPointsCounterTextUIPrefab;

	private PlayersDataManager playersDataManager;
	private StageStateManager stageStateManager;
	private StageFlowManager stageFlowManager;
	private TranslationBackgroundPanelUI translationBackgroundPanelUI;
	private PauseTextUI pauseTextUI;

	private void Awake()
	{
		playersDataManager = FindFirstObjectByType<PlayersDataManager>(FindObjectsInactive.Include);
		stageStateManager = FindFirstObjectByType<StageStateManager>(FindObjectsInactive.Include);
		stageFlowManager = FindFirstObjectByType<StageFlowManager>(FindObjectsInactive.Include);
		translationBackgroundPanelUI = FindFirstObjectByType<TranslationBackgroundPanelUI>(FindObjectsInactive.Include);
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

			if(stageFlowManager != null)
			{
				stageFlowManager.stageStartedEvent.AddListener(OnStageStarted);
				stageFlowManager.stageActivatedEvent.AddListener(OnStageActivated);
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

			if(stageFlowManager != null)
			{
				stageFlowManager.stageStartedEvent.RemoveListener(OnStageStarted);
				stageFlowManager.stageActivatedEvent.RemoveListener(OnStageActivated);
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
		
		var instance = Instantiate(gainedPointsCounterTextUIPrefab, canvasGO.transform);

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