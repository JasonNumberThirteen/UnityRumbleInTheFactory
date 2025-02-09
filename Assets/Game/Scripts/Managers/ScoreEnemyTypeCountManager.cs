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

	public void StartCounting(EnemyRobotData enemyRobotData, int numberOfDefeatedEnemyRobots)
	{
		numberOfCountedEnemies = currentScoreForDefeatedEnemies = 0;
		this.enemyRobotData = enemyRobotData;
		this.numberOfDefeatedEnemies = numberOfDefeatedEnemyRobots;

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

		if(enemyRobotData != null)
		{
			currentScoreForDefeatedEnemies += enemyRobotData.GetPointsForDefeat();
		}

		playerRobotScoreDataList.Clear();

		if(playerRobotsListData != null)
		{
			playerRobotsListData.ForEach(AddPlayerRobotScoreDataIfNeeded);
		}

		enemyCountedEvent?.Invoke(playerRobotScoreDataList);

		if(numberOfCountedEnemies >= numberOfDefeatedEnemies)
		{
			allEnemiesCountedEvent?.Invoke();
		}
		else
		{
			timer.StartTimer();
		}
	}

	private void AddPlayerRobotScoreDataIfNeeded(PlayerRobotData playerRobotData)
	{
		if(playerRobotData == null || !playerRobotData.DefeatedEnemies.TryGetValue(enemyRobotData, out var numberOfDefeatedEnemies) || numberOfDefeatedEnemies < numberOfCountedEnemies)
		{
			return;
		}

		var numberOfEnemiesToDisplay = Mathf.Min(numberOfCountedEnemies, numberOfDefeatedEnemies);
		var playerRobotScoreData = new PlayerRobotScoreData(playerRobotData, numberOfEnemiesToDisplay, currentScoreForDefeatedEnemies);

		playerRobotScoreDataList.Add(playerRobotScoreData);
	}
}