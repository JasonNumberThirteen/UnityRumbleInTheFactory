using UnityEngine;

public class RankBonusTriggerEventsReceiver : BonusTriggerEventsReceiver
{
	[SerializeField, Min(1)] private int ranks = 1;
	
	protected override void GiveEffect(GameObject sender)
	{
		if(sender.TryGetComponent(out RobotEntity robotEntity))
		{
			robotEntity.OnRankBonusCollected(ranks);
		}
	}
}