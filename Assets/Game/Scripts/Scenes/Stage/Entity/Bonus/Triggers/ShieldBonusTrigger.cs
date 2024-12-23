using UnityEngine;

public class ShieldBonusTrigger : TimedBonusTrigger
{
	public override void TriggerEffect(GameObject sender)
	{
		if(sender.TryGetComponent(out PlayerRobotShield prs))
		{
			prs.ShieldTimer.duration = GetDuration();

			prs.ShieldTimer.ResetTimer();
		}
		
		base.TriggerEffect(sender);
	}
}