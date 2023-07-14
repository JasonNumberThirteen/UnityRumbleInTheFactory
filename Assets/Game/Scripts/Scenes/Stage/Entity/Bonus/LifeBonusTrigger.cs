using UnityEngine;

public class LifeBonusTrigger : BonusTrigger
{
	public override void TriggerEffect(GameObject sender)
	{
		++playerData.Lives;

		base.TriggerEffect(sender);
	}
}