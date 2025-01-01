using UnityEngine;

public class ShieldBonusTrigger : TimedBonusTrigger
{
	public override void TriggerOnEnter(GameObject sender)
	{
		if(sender.TryGetComponent(out RobotShield prs))
		{
			prs.ShieldTimer.duration = GetDuration();

			prs.ShieldTimer.ResetTimer();
		}
		
		base.TriggerOnEnter(sender);
	}
}