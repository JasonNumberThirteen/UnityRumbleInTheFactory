using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Timer))]
public class RobotEntitiesDisablingManager : MonoBehaviour
{
	private Timer timer;
	private StageStateManager stageStateManager;
	private bool affectFriendly;
	
	public bool RobotsAreTemporarilyDisabled(bool checkFriendly) => timer.Started && checkFriendly == affectFriendly;
	
	public void DisableRobotEntitiesTemporarily(float duration, bool affectFriendly)
	{
		timer.duration = duration;
		this.affectFriendly = affectFriendly;

		timer.ResetTimer();
	}

	public void SetRobotEntitiesActive(bool active, bool affectFriendly)
	{
		var robotEntities = ObjectMethods.FindComponentsOfType<RobotEntity>(false).Where(robotEntity => robotEntity.IsFriendly() == affectFriendly);
		var robotEntityDisablers = robotEntities.Select(robotEntity => robotEntity.GetComponent<RobotEntityDisabler>()).Where(component => component != null);

		foreach (var robotEntityDisabler in robotEntityDisablers)
		{
			robotEntityDisabler.SetBehavioursActive(active);
		}
	}

	private void Awake()
	{
		timer = GetComponent<Timer>();
		stageStateManager = ObjectMethods.FindComponentOfType<StageStateManager>();

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
		SetRobotEntitiesActive(true, !affectFriendly);
		SetRobotEntitiesActive(false, affectFriendly);
	}

	private void OnTimerEnd()
	{
		SetRobotEntitiesActive(true, false);

		if(stageStateManager != null && !stageStateManager.StateIsSetTo(StageState.Over))
		{
			SetRobotEntitiesActive(true, true);
		}
	}

	private void OnStageStateChanged(StageState stageState)
	{
		if(stageState == StageState.Over)
		{
			SetRobotEntitiesActive(false, true);
		}
	}
}