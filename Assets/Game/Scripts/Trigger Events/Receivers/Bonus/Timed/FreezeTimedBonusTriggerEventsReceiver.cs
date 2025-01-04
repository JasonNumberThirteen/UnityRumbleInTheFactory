using UnityEngine;

public class FreezeTimedBonusTriggerEventsReceiver : TimedBonusTriggerEventsReceiver
{
	private RobotDisablingManager robotDisablingManager;
	
	public override void TriggerOnEnter(GameObject sender)
	{
		if(robotDisablingManager != null && sender.TryGetComponent(out Robot robot))
		{
			robotDisablingManager.DisableRobotsTemporarily(GetDuration(), !robot.IsFriendly());
		}

		base.TriggerOnEnter(sender);
	}

	private void Awake()
	{
		robotDisablingManager = FindAnyObjectByType<RobotDisablingManager>(FindObjectsInactive.Include);
	}
}