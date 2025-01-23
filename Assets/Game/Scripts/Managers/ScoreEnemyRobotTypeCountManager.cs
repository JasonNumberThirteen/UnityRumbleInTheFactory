using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Timer))]
public class ScoreEnemyRobotTypeCountManager : MonoBehaviour
{
	public UnityEvent<List<PlayerRobotScoreData>> enemyRobotCountedEvent;
	public UnityEvent allEnemyRobotsCountedEvent;

	[SerializeField] private PlayerRobotsListData playerRobotsListData;
	
	private Timer timer;
	private int numberOfCountedEnemyRobots;
	private int numberOfDefeatedEnemyRobots;
	private int currentScoreForDefeatedEnemyRobots;
	private EnemyRobotData enemyRobotData;
	private readonly List<PlayerRobotScoreData> playerRobotScoreDataList = new();

	public void StartCounting(EnemyRobotData enemyRobotData, int numberOfDefeatedEnemyRobots)
	{
		numberOfCountedEnemyRobots = currentScoreForDefeatedEnemyRobots = 0;
		this.enemyRobotData = enemyRobotData;
		this.numberOfDefeatedEnemyRobots = numberOfDefeatedEnemyRobots;

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
		++numberOfCountedEnemyRobots;

		if(enemyRobotData != null)
		{
			currentScoreForDefeatedEnemyRobots += enemyRobotData.GetPointsForDefeat();
		}

		playerRobotScoreDataList.Clear();

		if(playerRobotsListData != null)
		{
			playerRobotsListData.ForEach(AddPlayerRobotScoreDataIfNeeded);
		}

		enemyRobotCountedEvent?.Invoke(playerRobotScoreDataList);

		if(numberOfCountedEnemyRobots >= numberOfDefeatedEnemyRobots)
		{
			allEnemyRobotsCountedEvent?.Invoke();
		}
		else
		{
			timer.StartTimer();
		}
	}

	private void AddPlayerRobotScoreDataIfNeeded(PlayerRobotData playerRobotData)
	{
		if(playerRobotData == null || !playerRobotData.DefeatedEnemies.TryGetValue(enemyRobotData, out var numberOfDefeatedEnemies) || numberOfDefeatedEnemies < numberOfCountedEnemyRobots)
		{
			return;
		}

		var numberOfEnemyRobotsToDisplay = Mathf.Min(numberOfCountedEnemyRobots, numberOfDefeatedEnemies);
		var playerRobotScoreData = new PlayerRobotScoreData(playerRobotData, numberOfEnemyRobotsToDisplay, currentScoreForDefeatedEnemyRobots);

		playerRobotScoreDataList.Add(playerRobotScoreData);
	}
}