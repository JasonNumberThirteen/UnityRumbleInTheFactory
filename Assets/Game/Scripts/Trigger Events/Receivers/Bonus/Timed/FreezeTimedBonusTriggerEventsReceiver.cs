using UnityEngine;

public class FreezeTimedBonusTriggerEventsReceiver : TimedBonusTriggerEventsReceiver
{
	private RobotEntitiesDisablingManager robotEntitiesDisablingManager;
	
	protected override void GiveEffect(GameObject sender)
	{
		if(robotEntitiesDisablingManager != null && sender.TryGetComponent(out RobotEntity robotEntity))
		{
			robotEntitiesDisablingManager.DisableRobotEntitiesTemporarily(GetDuration(), !robotEntity.IsFriendly());
		}

		base.TriggerOnEnter(sender);
	}

	protected override void Awake()
	{
		base.Awake();
		
		robotEntitiesDisablingManager = FindAnyObjectByType<RobotEntitiesDisablingManager>(FindObjectsInactive.Include);
	}
}