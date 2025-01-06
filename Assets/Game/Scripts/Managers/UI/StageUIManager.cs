using UnityEngine;

public class StageUIManager : MonoBehaviour
{
	[SerializeField] private GameObject canvasGO;
	[SerializeField] private GameObject pauseTextUIGO;
	[SerializeField] private PlayerData[] playersData;
	[SerializeField] private IntCounter[] playerLivesCounters;
	[SerializeField] private IntCounter stageCounterInHeader;
	[SerializeField] private IntCounter stageCounterInPanelUI;
	[SerializeField] private GameData gameData;
	[SerializeField] private GainedPointsCounterTextUI gainedPointsCounterTextUIPrefab;

	private PlayersDataManager playersDataManager;
	private StageStateManager stageStateManager;

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
		for (int i = 0; i < playersData.Length; ++i)
		{
			playerLivesCounters[i].SetTo(playersData[i].Lives);
		}
	}

	private void Awake()
	{
		playersDataManager = FindFirstObjectByType<PlayersDataManager>(FindObjectsInactive.Include);
		stageStateManager = FindFirstObjectByType<StageStateManager>(FindObjectsInactive.Include);

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
			instance.Setup(position*16, points);
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