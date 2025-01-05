using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Timer))]
public class RobotEntitiesDisablingManager : MonoBehaviour
{
	private Timer timer;
	private bool disableFriendly;

	private StageStateManager stageStateManager;
	
	public bool RobotsAreTemporarilyDisabled() => timer.Started;
	
	public void DisableRobotEntitiesTemporarily(float duration, bool disableFriendly)
	{
		timer.duration = duration;
		this.disableFriendly = disableFriendly;

		timer.ResetTimer();
	}

	public void SetRobotEntitiesActive(bool active, bool disableFriendly)
	{
		var robotEntities = FindObjectsByType<RobotEntity>(FindObjectsSortMode.None).Where(robot => robot.IsFriendly() == disableFriendly);
		var robotEntityDisablers = robotEntities.Select(robot => robot.GetComponent<RobotEntityDisabler>()).Where(component => component != null);

		foreach (var robotEntityDisabler in robotEntityDisablers)
		{
			robotEntityDisabler.SetBehavioursActive(active);
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
		SetRobotEntitiesActive(false, disableFriendly);
	}

	private void OnTimerEnd()
	{
		SetRobotEntitiesActive(true, disableFriendly);
	}

	private void OnStageStateChanged(StageState stageState)
	{
		if(stageState == StageState.Over)
		{
			SetRobotEntitiesActive(false, true);
		}
	}
}