using UnityEngine;

public class FreezeTimedBonusTriggerEventsReceiver : TimedBonusTriggerEventsReceiver
{
	private RobotEntitiesDisablingManager robotEntitiesDisablingManager;
	
	public override void TriggerOnEnter(GameObject sender)
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