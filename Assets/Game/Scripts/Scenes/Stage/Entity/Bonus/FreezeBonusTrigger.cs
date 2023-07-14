using UnityEngine;

public class FreezeBonusTrigger : TimedBonusTrigger
{
	public override void TriggerEffect(GameObject sender)
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		
		foreach (GameObject enemy in enemies)
		{
			EnemyRobotFreeze erf = enemy.GetComponent<EnemyRobotFreeze>();

			erf.Freeze(duration);
		}

		base.TriggerEffect(sender);
	}
}