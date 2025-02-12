using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Timer))]
public class RobotEntitiesDisablingManager : MonoBehaviour
{
	private Timer timer;
	private StageStateManager stageStateManager;
	private bool affectFriendly;
	
	public bool RobotsAreTemporarilyDisabled(bool checkFriendly) => timer.TimerWasStarted && checkFriendly == affectFriendly;
	
	public void DisableRobotEntitiesTemporarily(float duration, bool affectFriendly)
	{
		this.affectFriendly = affectFriendly;

		timer.SetDuration(duration);
		timer.StartTimer();
	}

	public void SetRobotEntitiesActive(bool active, bool affectFriendly)
	{
		var robotEntities = ObjectMethods.FindComponentsOfType<RobotEntity>(false).Where(robotEntity => robotEntity.IsFriendly() == affectFriendly);
		var robotEntityDisablers = robotEntities.Select(robotEntity => robotEntity.GetComponent<RobotEntityDisabler>()).Where(component => component != null).ToArray();

		robotEntityDisablers.ForEach(robotEntityDisabler => robotEntityDisabler.SetBehavioursActive(active));
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
			timer.timerStartedEvent.AddListener(OnTimerStarted);
			timer.timerFinishedEvent.AddListener(OnTimerFinished);

			if(stageStateManager != null)
			{
				stageStateManager.stageStateChangedEvent.AddListener(OnStageStateChanged);
			}
		}
		else
		{
			timer.timerStartedEvent.RemoveListener(OnTimerStarted);
			timer.timerFinishedEvent.RemoveListener(OnTimerFinished);

			if(stageStateManager != null)
			{
				stageStateManager.stageStateChangedEvent.RemoveListener(OnStageStateChanged);
			}
		}
	}

	private void OnTimerStarted()
	{
		SetRobotEntitiesActive(true, !affectFriendly);
		SetRobotEntitiesActive(false, affectFriendly);
	}

	private void OnTimerFinished()
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