using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Timer))]
public class ScoreEnemyRobotTypeSwitchManager : MonoBehaviour
{
	public UnityEvent<int> enemyRobotTypeSwitchedEvent;
	public UnityEvent lastEnemyRobotTypeReachedEvent;
	
	private Timer timer;
	private int currentEnemyRobotTypeIndex;
	private int numberOfDefeatedEnemyRobotTypes;

	public void GoToNextEnemyRobotType()
	{
		timer.StartTimer();
	}

	private void Awake()
	{
		timer = GetComponent<Timer>();
		numberOfDefeatedEnemyRobotTypes = GetNumberOfDefeatedEnemyRobotTypes();

		RegisterToListeners(true);
	}

	private int GetNumberOfDefeatedEnemyRobotTypes()
	{
		var playersDefeatedEnemiesSumContainer = ObjectMethods.FindComponentOfType<PlayersDefeatedEnemiesSumContainer>();

		return playersDefeatedEnemiesSumContainer != null ? playersDefeatedEnemiesSumContainer.DefeatedEnemies.Count : 0;
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
		if(currentEnemyRobotTypeIndex < numberOfDefeatedEnemyRobotTypes)
		{
			enemyRobotTypeSwitchedEvent?.Invoke(currentEnemyRobotTypeIndex++);
		}
		else
		{
			lastEnemyRobotTypeReachedEvent?.Invoke();
		}
	}
}