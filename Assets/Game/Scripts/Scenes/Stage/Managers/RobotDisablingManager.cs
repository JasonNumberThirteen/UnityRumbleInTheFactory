using UnityEngine;

[RequireComponent(typeof(Timer))]
public class RobotDisablingManager : MonoBehaviour
{
	private Timer timer;
	
	public bool EnemiesAreFrozen() => timer.Started;
	
	public void InitiateFreeze(float duration)
	{
		timer.duration = duration;

		timer.ResetTimer();
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
			timer.onReset.AddListener(OnTimerReset);
			timer.onEnd.AddListener(OnTimerEnd);
		}
		else
		{
			timer.onReset.RemoveListener(OnTimerReset);
			timer.onEnd.RemoveListener(OnTimerEnd);
		}
	}

	private void OnTimerReset()
	{
		SetEnemiesFreeze(true);
	}

	private void OnTimerEnd()
	{
		SetEnemiesFreeze(false);
	}

	private void SetEnemiesFreeze(bool freeze)
	{
		var enemyRobotFreezeComponents = FindObjectsByType<EnemyRobotFreeze>(FindObjectsSortMode.None);

		foreach (var enemyRobotFreeze in enemyRobotFreezeComponents)
		{
			enemyRobotFreeze.SetFreezeState(freeze);
		}
	}
}