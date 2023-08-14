using UnityEngine;

public class BonusEnemyRobotTrigger : RobotTrigger
{
	public override void TriggerEffect(GameObject sender)
	{
		if(TryGetComponent(out EnemyRobotBonus erb))
		{
			erb.SpawnBonus();
		}

		base.TriggerEffect(sender);
	}
}