using UnityEngine;

public class RankBonusTrigger : BonusTrigger
{
	public override void TriggerEffect(GameObject sender)
	{
		++playerData.Rank;

		PlayerRobotRank prr = sender.GetComponent<PlayerRobotRank>();

		prr.SetRank();
		base.TriggerEffect(sender);
	}
}