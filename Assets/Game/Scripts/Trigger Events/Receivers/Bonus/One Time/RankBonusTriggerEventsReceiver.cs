using UnityEngine;

public class RankBonusTriggerEventsReceiver : BonusTriggerEventsReceiver
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