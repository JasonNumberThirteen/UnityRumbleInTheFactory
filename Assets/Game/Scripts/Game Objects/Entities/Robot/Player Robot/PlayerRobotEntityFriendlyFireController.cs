using UnityEngine;

[RequireComponent(typeof(Timer))]
public class PlayerRobotEntityFriendlyFireController : MonoBehaviour
{
	[SerializeField, Min(0.01f)] private float blinkTime = 0.125f;
	
	private Timer timer;
	private PlayerRobotEntity playerRobotEntity;
	private RobotEntityDisabler robotEntityDisabler;
	private PlayerRobotEntityRendererBlinker playerRobotEntityRendererBlinker;
	private StageStateManager stageStateManager;
	private StageEventsManager stageEventsManager;
	
	public void ImmobiliseTemporarily()
	{
		timer.StartTimer();
	}

	private void Awake()
	{
		timer = GetComponent<Timer>();
		playerRobotEntity = GetComponentInParent<PlayerRobotEntity>();
		robotEntityDisabler = GetComponentInParent<RobotEntityDisabler>();
		playerRobotEntityRendererBlinker = GetComponentInParent<PlayerRobotEntityRendererBlinker>();
		stageStateManager = ObjectMethods.FindComponentOfType<StageStateManager>();
		stageEventsManager = ObjectMethods.FindComponentOfType<StageEventsManager>();

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
		}
		else
		{
			timer.timerStartedEvent.RemoveListener(OnTimerStarted);
			timer.timerFinishedEvent.RemoveListener(OnTimerFinished);
		}
	}

	private void OnTimerStarted()
	{
		SetImmobilisationState(true);
	}

	private void OnTimerFinished()
	{
		SetImmobilisationState(false);
	}

	private void SetImmobilisationState(bool active)
	{
		SetDisablerActiveIfPossible(active);
		SetRendererBlinkerActive(active);
		SendEvent(active);
	}

	private void SetDisablerActiveIfPossible(bool active)
	{
		if(robotEntityDisabler == null)
		{
			return;
		}
		
		var canBeTriggered = stageStateManager == null || !stageStateManager.StateIsSetTo(StageState.Over);
		
		robotEntityDisabler.SetBehavioursActive(!active && canBeTriggered);
	}

	private void SetRendererBlinkerActive(bool active)
	{
		if(playerRobotEntityRendererBlinker != null)
		{
			playerRobotEntityRendererBlinker.SetBlinkActive(active, blinkTime);
		}
	}

	private void SendEvent(bool active)
	{
		if(stageEventsManager != null)
		{
			stageEventsManager.SendEvent(new RobotEntitiesDisablingStageEvent(StageEventType.PlayerRobotActivationStateWasChangedByFriendlyFire, active, new RobotEntity[]{playerRobotEntity}));
		}
	}
}