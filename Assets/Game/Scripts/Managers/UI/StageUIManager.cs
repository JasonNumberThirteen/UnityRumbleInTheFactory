using UnityEngine;

public class StageUIManager : MonoBehaviour
{
	[SerializeField] private GameObject canvasGO;
	[SerializeField] private GameObject pauseTextUIGO;
	[SerializeField] private IntCounter stageCounterInHeader;
	[SerializeField] private IntCounter stageCounterInPanelUI;
	[SerializeField] private GameData gameData;
	[SerializeField] private GainedPointsCounterTextUI gainedPointsCounterTextUIPrefab;

	private PlayersDataManager playersDataManager;
	private StageStateManager stageStateManager;
	private PlayerLivesCounterPanelUI[] playerLivesCounterPanelUIs;

	public void UpdateCounters()
	{
		UpdateLivesCounters();

		if(stageCounterInPanelUI != null && gameData != null)
		{
			stageCounterInPanelUI.SetTo(gameData.StageNumber);
		}
	}

	public void UpdateLivesCounters()
	{
		for (int i = 0; i < playerLivesCounterPanelUIs.Length; ++i)
		{
			playerLivesCounterPanelUIs[i].UpdateCounterIfPossible();
		}
	}

	private void Awake()
	{
		playersDataManager = FindFirstObjectByType<PlayersDataManager>(FindObjectsInactive.Include);
		stageStateManager = FindFirstObjectByType<StageStateManager>(FindObjectsInactive.Include);
		playerLivesCounterPanelUIs = FindObjectsByType<PlayerLivesCounterPanelUI>(FindObjectsSortMode.None);

		RegisterToListeners(true);
	}

	private void Start()
	{
		if(stageCounterInHeader != null && gameData != null)
		{
			stageCounterInHeader.SetTo(gameData.StageNumber);
		}
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

	private void OnStageStateChanged(StageState stageState)
	{
		if(pauseTextUIGO != null)
		{
			pauseTextUIGO.SetActive(stageStateManager != null && stageStateManager.StateIsSetTo(StageState.Paused));
		}
	}
}