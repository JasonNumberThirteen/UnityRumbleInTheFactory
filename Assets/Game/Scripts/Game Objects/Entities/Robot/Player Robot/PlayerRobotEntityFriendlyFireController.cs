using UnityEngine;

[RequireComponent(typeof(Timer))]
public class PlayerRobotEntityFriendlyFireController : MonoBehaviour
{
	[SerializeField, Min(0.01f)] private float blinkTime = 0.125f;
	
	private Timer timer;
	private RobotEntityDisabler robotEntityDisabler;
	private PlayerRobotEntityRendererBlinker playerRobotEntityRendererBlinker;
	private PlayerRobotEntityShootController playerRobotEntityShootController;
	private StageStateManager stageStateManager;
	
	public void ImmobiliseTemporarily()
	{
		timer.StartTimer();
	}

	private void Awake()
	{
		timer = GetComponent<Timer>();
		robotEntityDisabler = GetComponentInParent<RobotEntityDisabler>();
		playerRobotEntityRendererBlinker = GetComponentInParent<PlayerRobotEntityRendererBlinker>();
		playerRobotEntityShootController = GetComponentInParent<PlayerRobotEntityShootController>();
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
		if(robotEntityDisabler == null)
		{
			return;
		}

		var canBeTriggered = stageStateManager == null || !stageStateManager.StateIsSetTo(StageState.Over);
		var excludedComponents = new Behaviour[]{playerRobotEntityShootController};
		
		robotEntityDisabler.SetBehavioursActive(!active && canBeTriggered, excludedComponents);
	}

	private void SetRendererBlinkerActive(bool active)
	{
		if(playerRobotEntityRendererBlinker != null)
		{
			playerRobotEntityRendererBlinker.SetBlinkActive(active, blinkTime);
		}
	}
}