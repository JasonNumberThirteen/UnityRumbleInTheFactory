using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Timer))]
public class ScoreEnemyTypeCountManager : MonoBehaviour
{
	public UnityEvent<List<PlayerRobotScoreData>> enemyCountedEvent;
	public UnityEvent allEnemiesCountedEvent;

	[SerializeField] private PlayerRobotsListData playerRobotsListData;
	
	private Timer timer;
	private int numberOfCountedEnemies;
	private int numberOfDefeatedEnemies;
	private int currentScoreForDefeatedEnemies;
	private EnemyRobotData enemyRobotData;
	private readonly List<PlayerRobotScoreData> playerRobotScoreDataList = new();

	public void StartCounting(EnemyRobotData enemyRobotData, int numberOfDefeatedEnemies)
	{
		numberOfCountedEnemies = currentScoreForDefeatedEnemies = 0;
		this.enemyRobotData = enemyRobotData;
		this.numberOfDefeatedEnemies = numberOfDefeatedEnemies;

		timer.StartTimer();
	}

	private void Awake()
	{
		timer = GetComponent<Timer>();
		
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
			timer.timerFinishedEvent.AddListener(OnTimerFinished);
		}
		else
		{
			timer.timerFinishedEvent.RemoveListener(OnTimerFinished);
		}
	}

	private void OnTimerFinished()
	{
		++numberOfCountedEnemies;

		AddScoreForSingleEnemy();
		playerRobotScoreDataList.Clear();
		AddPlayerScoreDataFromEveryPlayer();
		enemyCountedEvent?.Invoke(playerRobotScoreDataList);
		ExecuteActionDependingOnNumberOfCountedEnemies();
	}

	private void AddScoreForSingleEnemy()
	{
		if(enemyRobotData != null)
		{
			currentScoreForDefeatedEnemies += enemyRobotData.GetPointsForDefeat();
		}
	}

	private void AddPlayerScoreDataFromEveryPlayer()
	{
		if(playerRobotsListData != null)
		{
			playerRobotsListData.GetPlayerRobotsData().ForEach(AddPlayerScoreDataIfNeeded);
		}
	}

	private void AddPlayerScoreDataIfNeeded(PlayerRobotData playerRobotData)
	{
		if(playerRobotData == null || !playerRobotData.DefeatedEnemies.TryGetValue(enemyRobotData, out var numberOfDefeatedEnemies) || numberOfDefeatedEnemies < numberOfCountedEnemies)
		{
			return;
		}

		var numberOfEnemiesToDisplay = Mathf.Min(numberOfCountedEnemies, numberOfDefeatedEnemies);
		var playerRobotScoreData = new PlayerRobotScoreData(playerRobotData, numberOfEnemiesToDisplay, currentScoreForDefeatedEnemies);

		playerRobotScoreDataList.Add(playerRobotScoreData);
	}

	private void ExecuteActionDependingOnNumberOfCountedEnemies()
	{
		if(numberOfCountedEnemies >= numberOfDefeatedEnemies)
		{
			allEnemiesCountedEvent?.Invoke();
		}
		else
		{
			timer.StartTimer();
		}
	}
}