using UnityEngine;

[RequireComponent(typeof(Timer))]
public class PlayerRobotEntityFriendlyFireController : MonoBehaviour
{
	[SerializeField, Min(0.01f)] private float blinkTime = 0.125f;
	
	private Timer timer;
	private RobotEntityDisabler robotEntityDisabler;
	private PlayerRobotEntityRendererBlinker playerRobotEntityRendererBlinker;
	private StageStateManager stageStateManager;
	private RobotEntitiesDisablingManager robotEntitiesDisablingManager;
	
	public void ImmobiliseTemporarily()
	{
		timer.StartTimer();
	}

	private void Awake()
	{
		timer = GetComponent<Timer>();
		robotEntityDisabler = GetComponentInParent<RobotEntityDisabler>();
		playerRobotEntityRendererBlinker = GetComponentInParent<PlayerRobotEntityRendererBlinker>();
		stageStateManager = ObjectMethods.FindComponentOfType<StageStateManager>();
		robotEntitiesDisablingManager = ObjectMethods.FindComponentOfType<RobotEntitiesDisablingManager>();

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
	}

	private void SetDisablerActiveIfPossible(bool active)
	{
		var playerRobotsAreStillDisabled = robotEntitiesDisablingManager != null && robotEntitiesDisablingManager.RobotsAreTemporarilyDisabled(true);
		
		if(robotEntityDisabler == null || playerRobotsAreStillDisabled)
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
}