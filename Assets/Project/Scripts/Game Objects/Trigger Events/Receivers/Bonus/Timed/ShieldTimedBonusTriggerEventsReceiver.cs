using UnityEngine;

public class ShieldTimedBonusTriggerEventsReceiver : TimedBonusTriggerEventsReceiver
{
	protected override void GiveEffect(GameObject sender)
	{
		if(sender.TryGetComponent(out RobotEntity robotEntity))
		{
			robotEntity.ActivateShield(GetDuration());
		}
	}
}