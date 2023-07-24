using UnityEngine;

public class ShieldBonusTrigger : TimedBonusTrigger
{
	public override void TriggerEffect(GameObject sender)
	{
		PlayerRobotShield prs = sender.GetComponent<PlayerRobotShield>();

		if(prs != null)
		{
			prs.ShieldTimer.duration = duration;

			prs.ShieldTimer.ResetTimer();
		}
		
		base.TriggerEffect(sender);
	}
}