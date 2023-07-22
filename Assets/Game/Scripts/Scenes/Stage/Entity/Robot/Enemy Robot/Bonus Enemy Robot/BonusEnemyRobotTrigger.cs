using UnityEngine;

public class BonusEnemyRobotTrigger : RobotTrigger
{
	public override void TriggerEffect(GameObject sender)
	{
		EnemyRobotBonus bonus = GetComponent<EnemyRobotBonus>();
		
		if(bonus != null)
		{
			bonus.SpawnBonus();
		}

		base.TriggerEffect(sender);
	}
}