using UnityEngine;

public class ShieldBonusEffect : TimedBonusEffect
{
	public override void PerformEffect()
	{
		PlayerRobotShield prs = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerRobotShield>();

		prs.ShieldTimer.duration = duration;

		prs.ShieldTimer.ResetTimer();
	}
}