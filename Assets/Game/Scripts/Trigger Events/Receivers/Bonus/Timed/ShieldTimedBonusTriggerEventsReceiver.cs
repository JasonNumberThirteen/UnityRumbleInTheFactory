using UnityEngine;

public class ShieldTimedBonusTriggerEventsReceiver : TimedBonusTriggerEventsReceiver
{
	public override void TriggerOnEnter(GameObject sender)
	{
		if(sender.TryGetComponent(out RobotEntity robotEntity))
		{
			robotEntity.ActivateShield(GetDuration());
		}
		
		base.TriggerOnEnter(sender);
	}
}