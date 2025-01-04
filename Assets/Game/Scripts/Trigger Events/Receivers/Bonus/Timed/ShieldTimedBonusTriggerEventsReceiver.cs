using UnityEngine;

public class ShieldTimedBonusTriggerEventsReceiver : TimedBonusTriggerEventsReceiver
{
	public override void TriggerOnEnter(GameObject sender)
	{
		if(sender.TryGetComponent(out Robot robot))
		{
			robot.ActivateShield(GetDuration());
		}
		
		base.TriggerOnEnter(sender);
	}
}