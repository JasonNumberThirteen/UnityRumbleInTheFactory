using UnityEngine;

public class FreezeBonusEffect : TimedBonusEffect
{
	public override void PerformEffect()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		
		foreach (GameObject enemy in enemies)
		{
			EnemyRobotFreeze erf = enemy.GetComponent<EnemyRobotFreeze>();

			erf.Freeze(duration);
		}
	}
}