using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Timer))]
public class RobotDisablingManager : MonoBehaviour
{
	private Timer timer;
	private bool disableFriendly;

	private StageStateManager stageStateManager;
	
	public bool RobotsAreTemporarilyDisabled() => timer.Started;
	
	public void DisableRobotsTemporarily(float duration, bool disableFriendly)
	{
		timer.duration = duration;
		this.disableFriendly = disableFriendly;

		timer.ResetTimer();
	}

	public void SetRobotsActive(bool active, bool disableFriendly)
	{
		var robots = FindObjectsByType<Robot>(FindObjectsSortMode.None).Where(robot => robot.IsFriendly() == disableFriendly);
		var robotDisablers = robots.Select(robot => robot.GetComponent<RobotDisabler>()).Where(component => component != null);

		foreach (var robotDisabler in robotDisablers)
		{
			robotDisabler.SetBehavioursActive(active);
		}
	}

	private void Awake()
	{
		timer = GetComponent<Timer>();
		stageStateManager = FindAnyObjectByType<StageStateManager>(FindObjectsInactive.Include);

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

			if(stageStateManager != null)
			{
				stageStateManager.stageStateChangedEvent.AddListener(OnStageStateChanged);
			}
		}
		else
		{
			timer.onReset.RemoveListener(OnTimerReset);
			timer.onEnd.RemoveListener(OnTimerEnd);

			if(stageStateManager != null)
			{
				stageStateManager.stageStateChangedEvent.RemoveListener(OnStageStateChanged);
			}
		}
	}

	private void OnTimerReset()
	{
		SetRobotsActive(false, disableFriendly);
	}

	private void OnTimerEnd()
	{
		SetRobotsActive(true, disableFriendly);
	}

	private void OnStageStateChanged(StageState stageState)
	{
		if(stageState == StageState.Over)
		{
			SetRobotsActive(false, true);
		}
	}
}