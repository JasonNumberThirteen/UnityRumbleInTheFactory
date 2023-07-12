using UnityEngine;

public class ShieldBonusEffect : BonusEffect
{
	[Min(0.01f)] public float duration = 10f;
	
	public override void PerformEffect()
	{
		PlayerRobotShield prs = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerRobotShield>();

		prs.ShieldTimer.duration = duration;

		prs.ShieldTimer.ResetTimer();
	}
}