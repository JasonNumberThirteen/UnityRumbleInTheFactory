using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ScoreUIManager : UIManager
{
	[SerializeField] private GameObject totalTextUIPrefab;
	[SerializeField] private GameObject emptyTotalTextUIPrefab;
	[SerializeField] private PlayerRobotsListData playerRobotsListData;

	private ScoreEnemyTypeSwitchManager scoreEnemyTypeSwitchManager;
	private ScoreEnemyTypeCountManager scoreEnemyTypeCountManager;
	private DefeatedEnemyTypesPanelUI defeatedEnemyTypesPanelUI;
	private PlayersTotalDefeatedEnemiesCountersPanelUI playersTotalDefeatedEnemiesCountersPanelUI;
	private DefeatedEnemyTypeIntCounterPanelUI currentDefeatedEnemyTypeIntCounterPanelUI;
	private Dictionary<PlayerRobotData, DefeatedEnemiesScoreIntCounterPanelUI> defeatedEnemiesScoreIntCounterPanelUIByPlayerRobotData;
	private EnemyRobotData[] defeatedEnemyTypesData;
	private List<PlayerScoreDetailsPanelUI> playerScoreDetailsPanelUIs;

	private void Awake()
	{
		scoreEnemyTypeSwitchManager = ObjectMethods.FindComponentOfType<ScoreEnemyTypeSwitchManager>();
		scoreEnemyTypeCountManager = ObjectMethods.FindComponentOfType<ScoreEnemyTypeCountManager>();
		defeatedEnemyTypesPanelUI = ObjectMethods.FindComponentOfType<DefeatedEnemyTypesPanelUI>();
		playerScoreDetailsPanelUIs = ObjectMethods.FindComponentsOfType<PlayerScoreDetailsPanelUI>(false).OrderBy(playerScoreDetailsPanelUI => playerScoreDetailsPanelUI.GetOrdinalNumber()).ToList();

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
			if(scoreEnemyTypeSwitchManager != null)
			{
				scoreEnemyTypeSwitchManager.enemyTypeSwitchedEvent.AddListener(OnEnemyTypeSwitched);
				scoreEnemyTypeSwitchManager.lastEnemyTypeReachedEvent.AddListener(OnLastEnemyTypeReached);
			}

			if(scoreEnemyTypeCountManager != null)
			{
				scoreEnemyTypeCountManager.enemyCountedEvent.AddListener(OnEnemyCounted);
				scoreEnemyTypeCountManager.allEnemiesCountedEvent.AddListener(OnAllEnemiesCounted);
			}
		}
		else
		{
			if(scoreEnemyTypeSwitchManager != null)
			{
				scoreEnemyTypeSwitchManager.enemyTypeSwitchedEvent.RemoveListener(OnEnemyTypeSwitched);
				scoreEnemyTypeSwitchManager.lastEnemyTypeReachedEvent.RemoveListener(OnLastEnemyTypeReached);
			}

			if(scoreEnemyTypeCountManager != null)
			{
				scoreEnemyTypeCountManager.enemyCountedEvent.RemoveListener(OnEnemyCounted);
				scoreEnemyTypeCountManager.allEnemiesCountedEvent.RemoveListener(OnAllEnemiesCounted);
			}
		}
	}

	private void OnEnemyTypeSwitched(int currentEnemyRobotTypeIndex)
	{
		GoToNextDefeatedEnemiesScoreIntCounterPanelUIs(currentEnemyRobotTypeIndex);
		GoToNextDefeatedEnemyTypeIntCounterPanelUIIfPossible(currentEnemyRobotTypeIndex);
		StartCountingNextEnemyTypeIfPossible(currentEnemyRobotTypeIndex);
	}

	private void GoToNextDefeatedEnemiesScoreIntCounterPanelUIs(int currentEnemyRobotTypeIndex)
	{
		if(playerScoreDetailsPanelUIs != null)
		{
			defeatedEnemiesScoreIntCounterPanelUIByPlayerRobotData = playerScoreDetailsPanelUIs.ToDictionary(key => key.GetPlayerRobotData(), value => value.GetDefeatedEnemiesScoreIntCounterPanelUIByIndex(currentEnemyRobotTypeIndex));
		}
	}

	private void GoToNextDefeatedEnemyTypeIntCounterPanelUIIfPossible(int currentEnemyRobotTypeIndex)
	{
		if(defeatedEnemyTypesPanelUI != null)
		{
			currentDefeatedEnemyTypeIntCounterPanelUI = defeatedEnemyTypesPanelUI.GetDefeatedEnemyTypeIntCounterPanelUIByIndex(currentEnemyRobotTypeIndex);
		}
	}

	private void StartCountingNextEnemyTypeIfPossible(int currentEnemyRobotTypeIndex)
	{
		if(scoreEnemyTypeCountManager == null || defeatedEnemyTypesData == null || currentEnemyRobotTypeIndex >= defeatedEnemyTypesData.Length)
		{
			return;
		}

		var enemyRobotData = defeatedEnemyTypesData[currentEnemyRobotTypeIndex];
		var numberOfDefeatedEnemyRobots = GetHighestNumberOfDefeatedEnemies(currentEnemyRobotTypeIndex);

		scoreEnemyTypeCountManager.StartCounting(enemyRobotData, numberOfDefeatedEnemyRobots);
	}

	private int GetHighestNumberOfDefeatedEnemies(int currentEnemyRobotTypeIndex)
	{
		if(playerRobotsListData == null)
		{
			return 0;
		}

		return playerRobotsListData.Max(playerRobotData =>
		{
			if(playerRobotData == null || defeatedEnemyTypesData == null || currentEnemyRobotTypeIndex >= defeatedEnemyTypesData.Length)
			{
				return 0;
			}

			var enemyRobotData = defeatedEnemyTypesData[currentEnemyRobotTypeIndex];

			return playerRobotData.DefeatedEnemies.TryGetValue(enemyRobotData, out var numberOfDefeatedEnemies) ? numberOfDefeatedEnemies : 0;
		});
	}

	private void OnLastEnemyTypeReached()
	{
		AddTotalTextUIsIfPossible();

		if(playersTotalDefeatedEnemiesCountersPanelUI != null)
		{
			playersTotalDefeatedEnemiesCountersPanelUI.SetActive(true);
		}
	}

	private void AddTotalTextUIsIfPossible()
	{
		if(totalTextUIPrefab == null || playerScoreDetailsPanelUIs == null || playerScoreDetailsPanelUIs.Count == 0)
		{
			return;
		}
		
		Instantiate(totalTextUIPrefab, playerScoreDetailsPanelUIs.First().transform);

		if(emptyTotalTextUIPrefab != null && playerScoreDetailsPanelUIs.Count > 1)
		{
			playerScoreDetailsPanelUIs.Skip(1).ToList().ForEach(playerScoreDetailsPanelUI => Instantiate(emptyTotalTextUIPrefab, playerScoreDetailsPanelUI.transform));
		}
	}

	private void OnEnemyCounted(List<PlayerRobotScoreData> playerRobotScoreDataList)
	{
		playerRobotScoreDataList.ForEach(SetCurrentScoreForDefeatedEnemiesValueToCounterIfPossible);

		if(currentDefeatedEnemyTypeIntCounterPanelUI != null)
		{
			currentDefeatedEnemyTypeIntCounterPanelUI.SetValuesToCountersIfPossible(playerRobotScoreDataList);
		}
	}

	private void SetCurrentScoreForDefeatedEnemiesValueToCounterIfPossible(PlayerRobotScoreData playerRobotScoreData)
	{
		if(defeatedEnemiesScoreIntCounterPanelUIByPlayerRobotData.TryGetValue(playerRobotScoreData.PlayerRobotData, out var defeatedEnemiesScoreIntCounterPanelUI))
		{
			defeatedEnemiesScoreIntCounterPanelUI.SetValueToCounter(playerRobotScoreData.CurrentScoreForDefeatedEnemyRobots);
		}
	}

	private void OnAllEnemiesCounted()
	{
		if(scoreEnemyTypeSwitchManager != null)
		{
			scoreEnemyTypeSwitchManager.GoToNextEnemyType();
		}
	}

	private void Start()
	{
		GetDefeatedEnemyTypesFromAllPlayersIfPossible();

		playersTotalDefeatedEnemiesCountersPanelUI = ObjectMethods.FindComponentOfType<PlayersTotalDefeatedEnemiesCountersPanelUI>();
	}

	private void GetDefeatedEnemyTypesFromAllPlayersIfPossible()
	{
		var playersDefeatedEnemiesSumContainer = ObjectMethods.FindComponentOfType<PlayersDefeatedEnemiesSumContainer>();

		if(playersDefeatedEnemiesSumContainer != null)
		{
			defeatedEnemyTypesData = playersDefeatedEnemiesSumContainer.DefeatedEnemies.Keys.ToArray();
		}
	}
}