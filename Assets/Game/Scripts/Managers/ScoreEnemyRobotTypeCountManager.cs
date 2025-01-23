using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Timer))]
public class ScoreEnemyRobotTypeCountManager : MonoBehaviour
{
	public UnityEvent<int, int> enemyRobotCountedEvent;
	public UnityEvent allEnemyRobotsCountedEvent;
	
	private Timer timer;
	private int numberOfCountedEnemyRobots;
	private int numberOfDefeatedEnemyRobots;
	private int currentScoreForDefeatedEnemyRobots;
	private int scorePerEnemyRobot;

	public void StartCounting(int numberOfDefeatedEnemyRobots, int scorePerEnemyRobot)
	{
		numberOfCountedEnemyRobots = currentScoreForDefeatedEnemyRobots = 0;
		this.numberOfDefeatedEnemyRobots = numberOfDefeatedEnemyRobots;
		this.scorePerEnemyRobot = scorePerEnemyRobot;

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
		currentScoreForDefeatedEnemyRobots += scorePerEnemyRobot;

		enemyRobotCountedEvent?.Invoke(numberOfCountedEnemyRobots, currentScoreForDefeatedEnemyRobots);

		if(numberOfCountedEnemyRobots == numberOfDefeatedEnemyRobots)
		{
			allEnemyRobotsCountedEvent?.Invoke();
		}
		else
		{
			timer.StartTimer();
		}
	}
}