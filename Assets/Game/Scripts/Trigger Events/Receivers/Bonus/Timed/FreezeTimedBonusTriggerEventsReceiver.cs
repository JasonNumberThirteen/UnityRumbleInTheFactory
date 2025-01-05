using UnityEngine;

public class FreezeTimedBonusTriggerEventsReceiver : TimedBonusTriggerEventsReceiver
{
	private RobotDisablingManager robotDisablingManager;
	
	public override void TriggerOnEnter(GameObject sender)
	{
		if(robotDisablingManager != null && sender.TryGetComponent(out RobotEntity robotEntity))
		{
			robotDisablingManager.DisableRobotEntitiesTemporarily(GetDuration(), !robotEntity.IsFriendly());
		}

		base.TriggerOnEnter(sender);
	}

	protected override void Awake()
	{
		base.Awake();
		
		robotDisablingManager = FindAnyObjectByType<RobotDisablingManager>(FindObjectsInactive.Include);
	}
}