using UnityEngine;

public class RankBonusTrigger : BonusTrigger
{
	public override void TriggerEffect(GameObject sender)
	{
		PlayerRobotRank prr = sender.GetComponent<PlayerRobotRank>();

		if(prr != null)
		{
			prr.Promote();
		}

		base.TriggerEffect(sender);
	}
}