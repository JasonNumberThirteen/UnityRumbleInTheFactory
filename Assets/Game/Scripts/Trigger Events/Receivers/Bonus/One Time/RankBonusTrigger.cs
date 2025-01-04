using UnityEngine;

public class RankBonusTrigger : BonusTrigger
{
	public override void TriggerOnEnter(GameObject sender)
	{
		if(sender.TryGetComponent(out PlayerRobotRankController playerRobotRank))
		{
			playerRobotRank.IncreaseRank();
		}

		base.TriggerOnEnter(sender);
	}
}