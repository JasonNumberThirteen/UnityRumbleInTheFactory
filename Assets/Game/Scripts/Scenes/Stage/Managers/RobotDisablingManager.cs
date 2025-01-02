using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Timer))]
public class RobotDisablingManager : MonoBehaviour
{
	private Timer timer;
	private bool controlFriendlyRobots;
	
	public bool RobotsAreTemporarilyDisabled() => timer.Started;
	
	public void DisableRobotsTemporarily(float duration, bool disableFriendly)
	{
		timer.duration = duration;
		controlFriendlyRobots = disableFriendly;

		timer.ResetTimer();
	}

	public void SetRobotsDisabled(bool freeze, bool disableFriendly)
	{
		var robots = FindObjectsByType<Robot>(FindObjectsSortMode.None).Where(robot => robot.IsFriendly() == disableFriendly);
		var enemyRobotFreezeComponents = robots.Select(robot => robot.GetComponent<EnemyRobotFreeze>()).Where(component => component != null);

		foreach (var enemyRobotFreeze in enemyRobotFreezeComponents)
		{
			enemyRobotFreeze.SetFreezeState(freeze);
		}
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
		SetRobotsDisabled(true, controlFriendlyRobots);
	}

	private void OnTimerEnd()
	{
		SetRobotsDisabled(false, controlFriendlyRobots);
	}
}