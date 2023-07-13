using UnityEngine;

public class FreezeBonusEffect : BonusEffect
{
	[Min(0.01f)] public float duration = 10f;
	
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