using UnityEngine;

public class RankBonusTrigger : BonusTrigger
{
	public override void TriggerEffect(GameObject sender)
	{
		++playerData.rank;

		base.TriggerEffect(sender);
	}
}