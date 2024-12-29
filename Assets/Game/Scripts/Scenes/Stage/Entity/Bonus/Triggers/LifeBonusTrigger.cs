using UnityEngine;

public class LifeBonusTrigger : BonusTrigger
{
	public override void TriggerOnEnter(GameObject sender)
	{
		if(sender.TryGetComponent(out PlayerRobotData playerRobotData))
		{
			++playerRobotData.Data.Lives;
		}

		base.TriggerOnEnter(sender);
	}
}