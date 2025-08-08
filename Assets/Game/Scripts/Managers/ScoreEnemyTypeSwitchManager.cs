using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Timer))]
public class ScoreEnemyTypeSwitchManager : MonoBehaviour
{
	public UnityEvent<int> enemyTypeWasSwitchedEvent;
	public UnityEvent lastEnemyTypeWasReachedEvent;
	
	private Timer timer;
	private int currentEnemyTypeIndex;
	private int numberOfDefeatedEnemyTypes;

	public void GoToNextEnemyType()
	{
		timer.StartTimer();
	}

	private void Awake()
	{
		timer = GetComponent<Timer>();
		numberOfDefeatedEnemyTypes = GetNumberOfDefeatedEnemyTypes();

		RegisterToListeners(true);
	}

	private int GetNumberOfDefeatedEnemyTypes()
	{
		var playersDefeatedEnemiesSumContainer = ObjectMethods.FindComponentOfType<PlayersDefeatedEnemiesSumContainer>();

		return playersDefeatedEnemiesSumContainer != null ? playersDefeatedEnemiesSumContainer.TotalDefeatedEnemies.Count : 0;
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
		if(currentEnemyTypeIndex < numberOfDefeatedEnemyTypes)
		{
			enemyTypeWasSwitchedEvent?.Invoke(currentEnemyTypeIndex++);
		}
		else
		{
			lastEnemyTypeWasReachedEvent?.Invoke();
		}
	}
}