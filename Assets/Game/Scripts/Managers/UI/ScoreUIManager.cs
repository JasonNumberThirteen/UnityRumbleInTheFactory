using System.Linq;
using UnityEngine;

public class ScoreUIManager : UIManager
{
	[SerializeField] private PlayerScoreDetailsPanelUI player1ScoreDetailsPanelUI;
	[SerializeField] private GameObject totalTextUIPrefab;

	private ScoreEnemyRobotTypeSwitchManager scoreEnemyRobotTypeSwitchManager;
	private ScoreEnemyRobotTypeCountManager scoreEnemyRobotTypeCountManager;
	private DefeatedEnemyRobotTypesPanelUI defeatedEnemyRobotTypesPanelUI;
	private PlayersTotalDefeatedEnemiesCountersPanelUI playersTotalDefeatedEnemiesCountersPanelUI;
	private DefeatedEnemyRobotTypeIntCounterPanelUI currentDefeatedEnemyRobotTypeIntCounterPanelUI;
	private DefeatedEnemiesScoreIntCounterPanelUI currentDefeatedEnemiesScoreIntCounterPanelUI;
	private int[] scoresPerEnemyRobotType;
	private int[] defeatedEnemiesCount;

	private void Awake()
	{
		scoreEnemyRobotTypeSwitchManager = ObjectMethods.FindComponentOfType<ScoreEnemyRobotTypeSwitchManager>();
		scoreEnemyRobotTypeCountManager = ObjectMethods.FindComponentOfType<ScoreEnemyRobotTypeCountManager>();
		defeatedEnemyRobotTypesPanelUI = ObjectMethods.FindComponentOfType<DefeatedEnemyRobotTypesPanelUI>();

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
		if(player1ScoreDetailsPanelUI != null)
		{
			currentDefeatedEnemiesScoreIntCounterPanelUI = player1ScoreDetailsPanelUI.GetDefeatedEnemiesScoreIntCounterPanelUIByIndex(currentEnemyRobotTypeIndex);
		}

		if(defeatedEnemyRobotTypesPanelUI != null)
		{
			currentDefeatedEnemyRobotTypeIntCounterPanelUI = defeatedEnemyRobotTypesPanelUI.GetDefeatedEnemyRobotTypeIntCounterPanelUIByIndex(currentEnemyRobotTypeIndex);
		}

		if(scoreEnemyRobotTypeCountManager != null)
		{
			scoreEnemyRobotTypeCountManager.StartCounting(defeatedEnemiesCount[currentEnemyRobotTypeIndex], scoresPerEnemyRobotType[currentEnemyRobotTypeIndex]);
		}
	}

	private void OnLastEnemyRobotTypeReached()
	{
		if(totalTextUIPrefab != null && player1ScoreDetailsPanelUI != null)
		{
			Instantiate(totalTextUIPrefab, player1ScoreDetailsPanelUI.transform);
		}

		if(playersTotalDefeatedEnemiesCountersPanelUI != null)
		{
			playersTotalDefeatedEnemiesCountersPanelUI.SetActive(true);
		}
	}

	private void OnEnemyRobotCounted(int numberOfCountedEnemyRobots, int currentScoreForDefeatedEnemyRobots)
	{
		if(currentDefeatedEnemiesScoreIntCounterPanelUI != null)
		{
			currentDefeatedEnemiesScoreIntCounterPanelUI.SetCounterValueTo(currentScoreForDefeatedEnemyRobots);
		}

		if(currentDefeatedEnemyRobotTypeIntCounterPanelUI != null)
		{
			currentDefeatedEnemyRobotTypeIntCounterPanelUI.SetCounterValueTo(numberOfCountedEnemyRobots);
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
		var playersDefeatedEnemiesSumContainer = ObjectMethods.FindComponentOfType<PlayersDefeatedEnemiesSumContainer>();

		if(playersDefeatedEnemiesSumContainer != null)
		{
			scoresPerEnemyRobotType = playersDefeatedEnemiesSumContainer.DefeatedEnemies.Keys.Select(key => key.GetPointsForDefeat()).ToArray();
			defeatedEnemiesCount = playersDefeatedEnemiesSumContainer.DefeatedEnemies.Values.ToArray();
		}

		playersTotalDefeatedEnemiesCountersPanelUI = ObjectMethods.FindComponentOfType<PlayersTotalDefeatedEnemiesCountersPanelUI>();
	}
}