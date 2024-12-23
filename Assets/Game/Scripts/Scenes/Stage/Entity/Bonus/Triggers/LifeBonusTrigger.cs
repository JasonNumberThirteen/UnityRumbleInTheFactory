using UnityEngine;

public class LifeBonusTrigger : BonusTrigger
{
	public override void TriggerEffect(GameObject sender)
	{
		if(sender.TryGetComponent(out PlayerRobotData playerRobotData))
		{
			++playerRobotData.Data.Lives;
		}

		base.TriggerEffect(sender);
	}
}