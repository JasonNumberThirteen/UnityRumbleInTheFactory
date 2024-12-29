using UnityEngine;

public class BonusEnemyRobotTrigger : RobotTrigger
{
	public override void TriggerOnEnter(GameObject sender)
	{
		if(TryGetComponent(out BonusEnemyRobotBonus berb))
		{
			berb.SpawnBonus();
		}

		if(TryGetComponent(out BonusEnemyRobotColor berc))
		{
			berc.RestoreInitialColor();
			Destroy(berc);
		}

		base.TriggerOnEnter(sender);
	}
}