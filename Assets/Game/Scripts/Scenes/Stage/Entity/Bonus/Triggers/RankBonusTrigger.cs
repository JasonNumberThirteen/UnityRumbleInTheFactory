using UnityEngine;

public class RankBonusTrigger : BonusTrigger
{
	public override void TriggerEffect(GameObject sender)
	{
		if(sender.TryGetComponent(out PlayerRobotRank prr))
		{
			prr.Promote();
		}

		base.TriggerEffect(sender);
	}
}