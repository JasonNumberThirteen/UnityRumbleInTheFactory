using UnityEngine;

public class RankBonusTriggerEventsReceiver : BonusTriggerEventsReceiver
{
	[SerializeField, Min(1)] private int ranks = 1;
	
	public override void TriggerOnEnter(GameObject sender)
	{
		if(sender.TryGetComponent(out RobotEntity robotEntity))
		{
			robotEntity.OnRankBonusCollected(ranks);
		}

		base.TriggerOnEnter(sender);
	}
}