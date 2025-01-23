using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ScoreUIManager : UIManager
{
	[SerializeField] private GameObject totalTextUIPrefab;
	[SerializeField] private PlayerRobotsListData playerRobotsListData;

	private ScoreEnemyRobotTypeSwitchManager scoreEnemyRobotTypeSwitchManager;
	private ScoreEnemyRobotTypeCountManager scoreEnemyRobotTypeCountManager;
	private DefeatedEnemyRobotTypesPanelUI defeatedEnemyRobotTypesPanelUI;
	private PlayersTotalDefeatedEnemiesCountersPanelUI playersTotalDefeatedEnemiesCountersPanelUI;
	private DefeatedEnemyRobotTypeIntCounterPanelUI currentDefeatedEnemyRobotTypeIntCounterPanelUI;
	private Dictionary<PlayerRobotData, DefeatedEnemiesScoreIntCounterPanelUI> defeatedEnemiesScoreIntCounterPanelUIByPlayerRobotData;
	private EnemyRobotData[] defeatedEnemyRobotTypesData;
	private List<PlayerScoreDetailsPanelUI> playerScoreDetailsPanelUIs;

	private void Awake()
	{
		scoreEnemyRobotTypeSwitchManager = ObjectMethods.FindComponentOfType<ScoreEnemyRobotTypeSwitchManager>();
		scoreEnemyRobotTypeCountManager = ObjectMethods.FindComponentOfType<ScoreEnemyRobotTypeCountManager>();
		defeatedEnemyRobotTypesPanelUI = ObjectMethods.FindComponentOfType<DefeatedEnemyRobotTypesPanelUI>();
		playerScoreDetailsPanelUIs = ObjectMethods.FindComponentsOfType<PlayerScoreDetailsPanelUI>().OrderBy(playerScoreDetailsPanelUI => playerScoreDetailsPanelUI.GetOrdinalNumber()).ToList();

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
			if(scoreEnemyRobotTypeSwitchManager != null)
			{
				scoreEnemyRobotTypeSwitchManager.enemyRobotTypeSwitchedEvent.AddListener(OnEnemyRobotTypeSwitched);
				scoreEnemyRobotTypeSwitchManager.lastEnemyRobotTypeReachedEvent.AddListener(OnLastEnemyRobotTypeReached);
			}

			if(scoreEnemyRobotTypeCountManager != null)
			{
				scoreEnemyRobotTypeCountManager.enemyRobotCountedEvent.AddListener(OnEnemyRobotCounted);
				scoreEnemyRobotTypeCountManager.allEnemyRobotsCountedEvent.AddListener(OnAllEnemyRobotsCounted);
			}
		}
		else
		{
			if(scoreEnemyRobotTypeSwitchManager != null)
			{
				scoreEnemyRobotTypeSwitchManager.enemyRobotTypeSwitchedEvent.RemoveListener(OnEnemyRobotTypeSwitched);
				scoreEnemyRobotTypeSwitchManager.lastEnemyRobotTypeReachedEvent.RemoveListener(OnLastEnemyRobotTypeReached);
			}

			if(scoreEnemyRobotTypeCountManager != null)
			{
				scoreEnemyRobotTypeCountManager.enemyRobotCountedEvent.RemoveListener(OnEnemyRobotCounted);
				scoreEnemyRobotTypeCountManager.allEnemyRobotsCountedEvent.RemoveListener(OnAllEnemyRobotsCounted);
			}
		}
	}

	private void OnEnemyRobotTypeSwitched(int currentEnemyRobotTypeIndex)
	{
		GoToNextDefeatedEnemiesScoreIntCounterPanelUIs(currentEnemyRobotTypeIndex);
		GoToNextDefeatedEnemyRobotTypeIntCounterPanelUIIfPossible(currentEnemyRobotTypeIndex);
		StartCountingNextEnemyRobotTypeIfPossible(currentEnemyRobotTypeIndex);
	}

	private void GoToNextDefeatedEnemiesScoreIntCounterPanelUIs(int currentEnemyRobotTypeIndex)
	{
		if(playerScoreDetailsPanelUIs != null)
		{
			defeatedEnemiesScoreIntCounterPanelUIByPlayerRobotData = playerScoreDetailsPanelUIs.ToDictionary(key => key.GetPlayerRobotData(), value => value.GetDefeatedEnemiesScoreIntCounterPanelUIByIndex(currentEnemyRobotTypeIndex));
		}
	}

	private void GoToNextDefeatedEnemyRobotTypeIntCounterPanelUIIfPossible(int currentEnemyRobotTypeIndex)
	{
		if(defeatedEnemyRobotTypesPanelUI != null)
		{
			currentDefeatedEnemyRobotTypeIntCounterPanelUI = defeatedEnemyRobotTypesPanelUI.GetDefeatedEnemyRobotTypeIntCounterPanelUIByIndex(currentEnemyRobotTypeIndex);
		}
	}

	private void StartCountingNextEnemyRobotTypeIfPossible(int currentEnemyRobotTypeIndex)
	{
		if(scoreEnemyRobotTypeCountManager == null || defeatedEnemyRobotTypesData == null || currentEnemyRobotTypeIndex >= defeatedEnemyRobotTypesData.Length)
		{
			return;
		}

		var enemyRobotData = defeatedEnemyRobotTypesData[currentEnemyRobotTypeIndex];
		var numberOfDefeatedEnemyRobots = GetHighestNumberOfDefeatedEnemies(currentEnemyRobotTypeIndex);

		scoreEnemyRobotTypeCountManager.StartCounting(enemyRobotData, numberOfDefeatedEnemyRobots);
	}

	private int GetHighestNumberOfDefeatedEnemies(int currentEnemyRobotTypeIndex)
	{
		if(playerRobotsListData == null)
		{
			return 0;
		}

		return playerRobotsListData.Max(playerRobotData =>
		{
			if(playerRobotData == null || defeatedEnemyRobotTypesData == null || currentEnemyRobotTypeIndex >= defeatedEnemyRobotTypesData.Length)
			{
				return 0;
			}

			var enemyRobotData = defeatedEnemyRobotTypesData[currentEnemyRobotTypeIndex];

			return playerRobotData.DefeatedEnemies.TryGetValue(enemyRobotData, out var numberOfDefeatedEnemies) ? numberOfDefeatedEnemies : 0;
		});
	}

	private void OnLastEnemyRobotTypeReached()
	{
		if(totalTextUIPrefab != null && playerScoreDetailsPanelUIs != null && playerScoreDetailsPanelUIs.Count > 0)
		{
			Instantiate(totalTextUIPrefab, playerScoreDetailsPanelUIs.First().transform);
		}

		if(playersTotalDefeatedEnemiesCountersPanelUI != null)
		{
			playersTotalDefeatedEnemiesCountersPanelUI.SetActive(true);
		}
	}

	private void OnEnemyRobotCounted(List<PlayerRobotScoreData> playerRobotScoreDataList)
	{
		playerRobotScoreDataList.ForEach(SetCurrentScoreForDefeatedEnemyRobotsValueToCounterIfPossible);

		if(currentDefeatedEnemyRobotTypeIntCounterPanelUI != null)
		{
			currentDefeatedEnemyRobotTypeIntCounterPanelUI.SetValuesToCountersIfPossible(playerRobotScoreDataList);
		}
	}

	private void SetCurrentScoreForDefeatedEnemyRobotsValueToCounterIfPossible(PlayerRobotScoreData playerRobotScoreData)
	{
		if(defeatedEnemiesScoreIntCounterPanelUIByPlayerRobotData.TryGetValue(playerRobotScoreData.PlayerRobotData, out var defeatedEnemiesScoreIntCounterPanelUI))
		{
			defeatedEnemiesScoreIntCounterPanelUI.SetValueToCounter(playerRobotScoreData.CurrentScoreForDefeatedEnemyRobots);
		}
	}

	private void OnAllEnemyRobotsCounted()
	{
		if(scoreEnemyRobotTypeSwitchManager != null)
		{
			scoreEnemyRobotTypeSwitchManager.GoToNextEnemyRobotType();
		}
	}

	private void Start()
	{
		GetDefeatedEnemyRobotTypesFromAllPlayersIfPossible();

		playersTotalDefeatedEnemiesCountersPanelUI = ObjectMethods.FindComponentOfType<PlayersTotalDefeatedEnemiesCountersPanelUI>();
	}

	private void GetDefeatedEnemyRobotTypesFromAllPlayersIfPossible()
	{
		var playersDefeatedEnemiesSumContainer = ObjectMethods.FindComponentOfType<PlayersDefeatedEnemiesSumContainer>();

		if(playersDefeatedEnemiesSumContainer != null)
		{
			defeatedEnemyRobotTypesData = playersDefeatedEnemiesSumContainer.DefeatedEnemies.Keys.ToArray();
		}
	}
}