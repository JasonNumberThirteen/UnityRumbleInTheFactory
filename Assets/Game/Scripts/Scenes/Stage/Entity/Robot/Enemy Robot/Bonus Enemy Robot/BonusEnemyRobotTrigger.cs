using UnityEngine;

public class BonusEnemyRobotTrigger : RobotTrigger
{
	public override void TriggerEffect(GameObject sender)
	{
		if(TryGetComponent(out BonusEnemyRobotBonus berb))
		{
			berb.SpawnBonus();
		}

		if(TryGetComponent(out BonusEnemyRobotColor berc))
		{
			Destroy(berc);
		}

		base.TriggerEffect(sender);
	}
}