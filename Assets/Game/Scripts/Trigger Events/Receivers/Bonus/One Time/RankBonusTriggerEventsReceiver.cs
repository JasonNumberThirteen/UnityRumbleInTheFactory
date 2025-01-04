using UnityEngine;

public class RankBonusTriggerEventsReceiver : BonusTriggerEventsReceiver
{
	public override void TriggerOnEnter(GameObject sender)
	{
		if(sender.TryGetComponent(out PlayerRobotRankController playerRobotRankController))
		{
			playerRobotRankController.IncreaseRank();
		}

		base.TriggerOnEnter(sender);
	}
}