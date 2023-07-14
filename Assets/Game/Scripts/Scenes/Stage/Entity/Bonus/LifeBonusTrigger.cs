using UnityEngine;

public class LifeBonusTrigger : BonusTrigger
{
	public override void TriggerEffect(GameObject sender)
	{
		++playerData.lives;

		base.TriggerEffect(sender);
	}
}