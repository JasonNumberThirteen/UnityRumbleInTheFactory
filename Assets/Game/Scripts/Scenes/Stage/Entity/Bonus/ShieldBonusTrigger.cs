using UnityEngine;

public class ShieldBonusTrigger : TimedBonusTrigger
{
	public override void TriggerEffect(GameObject sender)
	{
		PlayerRobotShield prs = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerRobotShield>();
		
		prs.ShieldTimer.duration = duration;

		prs.ShieldTimer.ResetTimer();
		base.TriggerEffect(sender);
	}
}