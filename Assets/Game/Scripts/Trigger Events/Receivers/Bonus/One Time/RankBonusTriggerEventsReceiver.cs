using UnityEngine;

public class RankBonusTriggerEventsReceiver : BonusTriggerEventsReceiver
{
	public override void TriggerOnEnter(GameObject sender)
	{
		if(sender.TryGetComponent(out PlayerRobotEntityRankController playerRobotEntityRankController))
		{
			playerRobotEntityRankController.IncreaseRank();
		}

		base.TriggerOnEnter(sender);
	}
}