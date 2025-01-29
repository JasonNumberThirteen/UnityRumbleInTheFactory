using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Timer))]
public class ScoreBonusPointsAwardManager : MonoBehaviour
{
	public UnityEvent playerAwardedWithPointsEvent;
	
	[SerializeField] private GameData gameData;
	[SerializeField] private PlayerRobotsListData playerRobotsListData;
	[SerializeField, Min(0)] private int numberOfPoints = 1000;
	[SerializeField] private BonusPointsCounterPanelUI bonusPointsCounterPanelUIPrefab;

	private Timer timer;
	private ScoreEnemyRobotTypeSwitchManager scoreEnemyRobotTypeSwitchManager;
	private PlayerScoreDetailsPanelUI playerScoreDetailsPanelUI;
	private PlayerRobotData playerRobotDataToAward;

	private void Awake()
	{
		if(playerRobotsListData == null || (gameData != null && gameData.GameIsOver))
		{
			return;
		}

		timer = GetComponent<Timer>();
		scoreEnemyRobotTypeSwitchManager = ObjectMethods.FindComponentOfType<ScoreEnemyRobotTypeSwitchManager>();

		DetermineIfAnyPlayerCanBeAwarded();
	}

	private void DetermineIfAnyPlayerCanBeAwarded()
	{
		var numberOfDefeatedEnemiesByPlayerRobot = playerRobotsListData.GetPlayerRobotsData().ToDictionary(key => key, value => value.DefeatedEnemies.Values.Sum());

		if(BonusPointsCanBeAwarded(numberOfDefeatedEnemiesByPlayerRobot.Values.ToList()))
		{
			FindComponentsOfAwardedPlayerIfPossible(numberOfDefeatedEnemiesByPlayerRobot);
		}
	}

	private void FindComponentsOfAwardedPlayerIfPossible(Dictionary<PlayerRobotData, int> numberOfDefeatedEnemiesByPlayerRobot)
	{
		var playerRobotWithHighestNumberOfDefeatedEnemies = numberOfDefeatedEnemiesByPlayerRobot.Aggregate((a, b) => a.Value > b.Value ? a : b).Key;

		playerRobotDataToAward = playerRobotWithHighestNumberOfDefeatedEnemies;
		playerScoreDetailsPanelUI = ObjectMethods.FindComponentsOfType<PlayerScoreDetailsPanelUI>().FirstOrDefault(playerScoreDetailsPanelUI => playerScoreDetailsPanelUI.GetPlayerRobotData() == playerRobotDataToAward);

		RegisterToListeners(true);
	}

	private void OnDestroy()
	{
		if(playerRobotDataToAward != null)
		{
			RegisterToListeners(false);
		}
	}

	private void RegisterToListeners(bool register)
	{
		if(register)
		{
			timer.timerFinishedEvent.AddListener(OnTimerFinished);

			if(scoreEnemyRobotTypeSwitchManager != null)
			{
				scoreEnemyRobotTypeSwitchManager.lastEnemyRobotTypeReachedEvent.AddListener(OnLastEnemyRobotTypeReached);
			}
		}
		else
		{
			timer.timerFinishedEvent.RemoveListener(OnTimerFinished);

			if(scoreEnemyRobotTypeSwitchManager != null)
			{
				scoreEnemyRobotTypeSwitchManager.lastEnemyRobotTypeReachedEvent.RemoveListener(OnLastEnemyRobotTypeReached);
			}
		}
	}

	private void OnTimerFinished()
	{
		if(bonusPointsCounterPanelUIPrefab == null || playerScoreDetailsPanelUI == null)
		{
			return;
		}

		Instantiate(bonusPointsCounterPanelUIPrefab, playerScoreDetailsPanelUI.transform).Setup(numberOfPoints);
		AwardPlayerIfPossible();
	}

	private void AwardPlayerIfPossible()
	{
		if(playerRobotDataToAward == null)
		{
			return;
		}
		
		playerRobotDataToAward.Score += numberOfPoints;

		UpdateAwardedPlayerScoreCounterIfPossible();
		playerAwardedWithPointsEvent?.Invoke();
	}

	private void UpdateAwardedPlayerScoreCounterIfPossible()
	{
		if(playerRobotDataToAward == null || playerScoreDetailsPanelUI == null)
		{
			return;
		}
		
		var playerScoreIntCounter = playerScoreDetailsPanelUI.GetComponentInChildren<PlayerScoreIntCounter>();

		if(playerScoreIntCounter != null)
		{
			playerScoreIntCounter.SetTo(playerRobotDataToAward.Score);
		}
	}
	
	private void OnLastEnemyRobotTypeReached()
	{
		timer.StartTimer();
	}

	private bool BonusPointsCanBeAwarded(List<int> numberOfDefeatedEnemiesPerPlayer)
	{
		if(numberOfDefeatedEnemiesPerPlayer == null || numberOfDefeatedEnemiesPerPlayer.Count < 2)
		{
			return false;
		}
		
		var numberOfEnemiesDefeatedByFirstPlayer = numberOfDefeatedEnemiesPerPlayer.First();

		return !numberOfDefeatedEnemiesPerPlayer.All(numberOfDefeatedEnemies => numberOfDefeatedEnemies == numberOfEnemiesDefeatedByFirstPlayer);
	}
}