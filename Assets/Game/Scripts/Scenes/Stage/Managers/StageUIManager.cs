using UnityEngine;

public class StageUIManager : MonoBehaviour
{
	public RectTransform parent;
	public GameObject gainedPointsCounter, pauseText;
	public GameData gameData;
	public PlayerData[] playersData;
	public IntCounter[] playerLivesCounters;
	public IntCounter stageCounterText, stageCounterIcon;
	public LeftEnemyIconsManager leftEnemyIconsManager;

	private PlayersDataManager playersDataManager;

	public void ControlPauseTextDisplay() => pauseText.SetActive(StageManager.instance.stateManager.StateIsSetTo(StageState.Paused));
	public void UpdateStageCounterIcon() => stageCounterIcon.SetTo(gameData.StageNumber);

	public void UpdateCounters()
	{
		UpdateLivesCounters();
		UpdateStageCounterIcon();
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

		RegisterToListeners(true);
	}

	private void Start()
	{
		stageCounterText.SetTo(gameData.StageNumber);
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
		}
		else
		{
			if(playersDataManager != null)
			{
				playersDataManager.playerScoreChangedEvent.RemoveListener(OnPlayerScoreChanged);
			}
		}
	}

	private void OnPlayerScoreChanged(int score, GameObject go)
	{
		InstantiateGainedPointsCounter(score, go.transform.position);
	}

	private void InstantiateGainedPointsCounter(int points, Vector2 position)
	{
		var instance = Instantiate(gainedPointsCounter, parent.transform);
		
		if(instance.TryGetComponent(out RectTransformPositionController rectTransformPositionController))
		{
			rectTransformPositionController.SetPosition(position*16);
		}

		if(instance.TryGetComponent(out IntCounter counter))
		{
			counter.SetTo(points);
		}
	}
}