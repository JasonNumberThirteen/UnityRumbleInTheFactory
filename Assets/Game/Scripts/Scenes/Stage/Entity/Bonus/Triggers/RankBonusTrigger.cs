using UnityEngine;

public class RankBonusTrigger : BonusTrigger
{
	public override void TriggerEffect(GameObject sender)
	{
		if(sender.TryGetComponent(out PlayerRobotRankController playerRobotRank))
		{
			playerRobotRank.Promote();
		}

		base.TriggerEffect(sender);
	}
}