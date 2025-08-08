using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Timer))]
public class ScoreBonusPointsAwardManager : MonoBehaviour
{
	public UnityEvent<bool> playerWasAwardedWithPointsEvent;
	
	[SerializeField] private GameData gameData;
	[SerializeField] private PlayerRobotsListData playerRobotsListData;
	[SerializeField, Min(0)] private int numberOfPoints = 1000;
	[SerializeField] private BonusPointsIntCounterPanelUI bonusPointsIntCounterPanelUIPrefab;

	private Timer timer;
	private ScoreEnemyTypeSwitchManager scoreEnemyTypeSwitchManager;
	private PlayerRobotData playerRobotDataToAward;
	private PlayerScoreDetailsPanelUI playerScoreDetailsPanelUI;

	private void Awake()
	{
		if(playerRobotsListData == null || GameDataMethods.GameIsOver(gameData))
		{
			return;
		}

		timer = GetComponent<Timer>();
		scoreEnemyTypeSwitchManager = ObjectMethods.FindComponentOfType<ScoreEnemyTypeSwitchManager>();

		DetermineIfAnyPlayerCanBeAwarded();
	}

	private void DetermineIfAnyPlayerCanBeAwarded()
	{
		var totalDefeatedEnemiesByPlayers = new TotalDefeatedEnemiesByPlayersDictionary(playerRobotsListData);

		if(BonusPointsCanBeAwarded(totalDefeatedEnemiesByPlayers.Values.ToList()))
		{
			FindComponentsOfAwardedPlayerIfPossible(totalDefeatedEnemiesByPlayers);
		}
	}

	private void FindComponentsOfAwardedPlayerIfPossible(TotalDefeatedEnemiesByPlayersDictionary totalDefeatedEnemiesByPlayers)
	{
		playerRobotDataToAward = totalDefeatedEnemiesByPlayers?.GetPlayerWithHighestNumberOfDefeatedEnemies();
		playerScoreDetailsPanelUI = ObjectMethods.FindComponentsOfType<PlayerScoreDetailsPanelUI>(false).FirstOrDefault(playerScoreDetailsPanelUI => playerScoreDetailsPanelUI.GetPlayerRobotData() == playerRobotDataToAward);

		RegisterToListeners(true);
	}

	private void OnDestroy()
	{
		RegisterToListeners(false);
	}

	private void RegisterToListeners(bool register)
	{
		if(playerRobotDataToAward == null)
		{
			return;
		}
		
		if(register)
		{
			timer.timerFinishedEvent.AddListener(OnTimerFinished);

			if(scoreEnemyTypeSwitchManager != null)
			{
				scoreEnemyTypeSwitchManager.lastEnemyTypeWasReachedEvent.AddListener(timer.StartTimer);
			}
		}
		else
		{
			timer.timerFinishedEvent.RemoveListener(OnTimerFinished);

			if(scoreEnemyTypeSwitchManager != null)
			{
				scoreEnemyTypeSwitchManager.lastEnemyTypeWasReachedEvent.RemoveListener(timer.StartTimer);
			}
		}
	}

	private void OnTimerFinished()
	{
		if(bonusPointsIntCounterPanelUIPrefab == null || playerScoreDetailsPanelUI == null)
		{
			return;
		}

		Instantiate(bonusPointsIntCounterPanelUIPrefab, playerScoreDetailsPanelUI.transform).SetValueToCounter(numberOfPoints);
		AwardPlayerIfPossible();
	}

	private void AwardPlayerIfPossible()
	{
		if(playerRobotDataToAward == null)
		{
			return;
		}

		var beatenHighScoreEarlier = GameDataMethods.BeatenHighScore(gameData);
		var gameDataIsDefined = GameDataMethods.GameDataIsDefined(gameData);
		
		playerRobotDataToAward.Score += numberOfPoints;

		if(gameDataIsDefined)
		{
			gameData.SetHighScoreIfPossible(playerRobotDataToAward.Score, () => ++playerRobotDataToAward.Lives);
		}

		UpdateAwardedPlayerScoreCounterIfPossible();
		playerWasAwardedWithPointsEvent?.Invoke(gameDataIsDefined && beatenHighScoreEarlier != gameData.BeatenHighScore);
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